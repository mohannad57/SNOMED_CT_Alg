using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Http;
using System.Net.Http.Headers;
using SNOMEDIDSelector.Misc;
using SNOMEDIDSelector.Models;
using SNOMEDIDSelector.Services;
using System.IO;

namespace SNOMEDCTSelector
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Holds a string value of the current processed SNOMED CT ID
        /// i.g. @"http://purl.bioontology.org/ontology/SNOMEDCT/230716006"
        /// </summary>
        public string CN_ID { get; set; }
        /// <summary>
        /// for each iteration holsd the and the stats for it.
        /// </summary>
        public Dictionary<int, Stats> ItrStats { get; set; }
        /// <summary>
        /// Stores the current iteration number 
        /// </summary>
        public int Itr { get; set; }
        /// <summary>
        /// Stores the Iteration's current node.
        /// </summary>
        public Dictionary<int, CTNode> ItrNode { get; set; }
        public CTNode CN { get; set; }
        /// <summary>
        /// A key value pair of SNOMED CT ID and its Prefered Label
        /// </summary>
        Dictionary<string, string> URI_PreferdName;
        public Form1()
        {
            InitializeComponent();
            URI_PreferdName = new Dictionary<string, string>();
            ItrStats = new Dictionary<int, Stats>();
            ItrNode = new Dictionary<int, CTNode>();
            btnShowCandIDs.Enabled = false;
            btnSaveStates.Enabled = false;
        }
        protected void InitNewSearch()
        {
            Itr = 0;
            ItrNode = new Dictionary<int, CTNode>
            {
                [Itr] = new CTNode()
            };
            ItrStats = new Dictionary<int, Stats>
            {
                [Itr] = new Stats()
            };
            //Reset lables 
            lblTH.Text = "...";
            lblIteration.Text = "...";
            lblNumOfChildren.Text = "...";
            lblNumOfCandidates.Text = "...";
            lblReductionPercent.Text = "...";
            pnlChildren.Controls.Clear();
            pnlContextQuestions.Controls.Clear();
            btnShowCandIDs.Enabled = false;
            btnSaveStates.Enabled = false;
            if (tvNodes.Nodes.Count > 0)
                tvNodes.Nodes.Clear();
        }
        /// <summary>
        /// Expands 
        /// </summary>
        /// <param name="id">SNOMED CT id e.g. 230716006 </param>
        private void Search(string id)
        {
            SearchQuery res = APIHandler.GetBioPortalSearch(id);
            if (res.Collection.Count() > 0)
            {
                CN = ItrNode[Itr];
                if (CN != null && CN.ID != res.Collection[0].Id.ToString())
                {
                    CN.ID = res.Collection[0].Id.ToString();
                    string shortID = CN.ID.Replace("http://purl.bioontology.org/ontology/SNOMEDCT/", "");
                    string prefLabel = res.Collection[0].Properties[Config.URI_prefLabel][0];
                    AddNewNode(shortID, prefLabel,Itr-1);
                    if (!URI_PreferdName.ContainsKey(CN.ID))
                        URI_PreferdName.Add(CN.ID, prefLabel);
                    GetChildern(res.Collection[0].Links.Children.ToString());
                }
            }
        }
        public void AddNewNode(string id, string name, int atDepth)
        {
            if (tvNodes.Nodes.Count == 0)
            {
                TreeNode root = new TreeNode($"{id} | {name}");
                tvNodes.Nodes.Add(root);
            }
            else
            {
                AddTreeNodeToGrandChild(tvNodes.Nodes[0], 0, atDepth, id, name);
            }
            tvNodes.ExpandAll();
        }

        public void AddTreeNodeToGrandChild(TreeNode nodeParent, int iDepth, int atDepth, string id, string name)
        {
            if (iDepth == atDepth)
            {
                // Create one child node for the given parent node
                TreeNode nodeChild = new TreeNode($"{id} | {name}");
                nodeParent.Nodes.Add(nodeChild);
            }
            else
            {
                AddTreeNodeToGrandChild(nodeParent.Nodes[0], ++iDepth, atDepth, id, name);
            }
        }

        public void ExpandChild(string url_id)
        {
            Itr++;
            ItrNode[Itr] = new CTNode();
            ItrStats[Itr] = new Stats();
            string id = url_id.Substring(url_id.LastIndexOf("SNOMEDCT/") + 9);
            Search(id);
        }

        private void GetChildern(string id)
        {
            Children children = APIHandler.getBioPortalChildren(id);
            foreach (var child in children.Collection)
            {
                CN.ChildrenScores.Add(child.Id.ToString(), 0);
                if (!URI_PreferdName.ContainsKey(child.Id.ToString()))
                    URI_PreferdName.Add(child.Id.ToString(), child.PrefLabel);
                // Add the child key to start adding the relations for each child
                if (!CN.ChildrenRelations.ContainsKey(child.Id.ToString()))
                    CN.ChildrenRelations.Add(child.Id.ToString(), new Dictionary<string, HashSet<string>>());
                GetProperties(child.Id.ToString());
            }
        }

        /// <summary>
        /// given a SNOMED CT id e.g. 230716006, this method will return the properties, 
        /// and set the Contextualized Questions
        /// </summary>
        /// <param name="id">i.g. http://purl.bioontology.org/ontology/SNOMEDCT/230716006 </param>
        private void GetProperties(string id)
        {
            string short_id = id.Substring(id.LastIndexOf('/') + 1);
            SearchQuery res = APIHandler.GetBioPortalSearch(short_id);
            if (res.Collection.Count() > 0)
            {
                string SearchID = res.Collection[0].Id.ToString();
                string prefLabel = res.Collection[0].Properties[Config.URI_prefLabel][0];
                if (!URI_PreferdName.ContainsKey(SearchID))
                    URI_PreferdName.Add(SearchID, prefLabel);
                ProcessRelations(id, res.Collection[0].Properties);
            }
        }
        /// <summary>
        /// Extracts the important relations that starts with has_
        /// i.e. has_finding_site,... etc. 
        /// </summary>
        /// <param name="id">i.g. http://purl.bioontology.org/ontology/SNOMEDCT/230716006 </param>
        /// <param name="properties">Dictionary of relation, list of objects as retrieved from the API</param>
        private void ProcessRelations(string id, Dictionary<string, string[]> properties)
        {
            foreach (KeyValuePair<string, string[]> kvp in properties)
            {
                if (kvp.Key.StartsWith(Config.URI_BASE_has_properti) || Config.URI_BASE_properties.Contains(kvp.Key))
                {
                    // add the question with falsed checked (not selected to be updated later
                    if (!CN.QuestionsChecked.ContainsKey(kvp.Key))
                        CN.QuestionsChecked.Add(kvp.Key, false);

                    // count the number of times a question/relation attend to children
                    if (CN.QuestionsCount.ContainsKey(kvp.Key))
                        CN.QuestionsCount[kvp.Key]++;
                    else
                        CN.QuestionsCount.Add(kvp.Key, 1);

                    // Store the relations and objects for each child, for scoring purposes later on.
                    if (!CN.ChildrenRelations[id].ContainsKey(kvp.Key))
                    {
                        CN.ChildrenRelations[id].Add(kvp.Key, new HashSet<string>(kvp.Value));
                    }
                    // this option/branch should be impossible to get to, 
                    // as the above should add all objects 
                    // and a relation don't repeat for a single child in the API results
                    else
                    {
                        foreach (string obj in kvp.Value)
                            CN.ChildrenRelations[id][kvp.Key].Add(obj);
                    }

                    // build the contextualized questions from all children comulitavily
                    if (!CN.ContextualizedQuestions.ContainsKey(kvp.Key))
                    {
                        CN.ContextualizedQuestions.Add(kvp.Key, new HashSet<string>(kvp.Value));
                        foreach (string obj in kvp.Value)
                        {
                            if (!URI_PreferdName.ContainsKey(obj))
                                URI_PreferdName.Add(obj, APIHandler.GetBioPortalClass(obj).PrefLabel);

                            // add the question with falsed checked (not selected to be updated later
                            if (!CN.AnswersChecked.ContainsKey(obj))
                                CN.AnswersChecked.Add(obj, false);

                            // count the number of times an answer/object attend to children
                            if (CN.AnswersCount.ContainsKey(obj))
                                CN.AnswersCount[obj]++;
                            else
                                CN.AnswersCount.Add(obj, 1);
                        }
                    }
                    // This option/branch is possible compared to the one above 
                    // because the ContextualizedQuestions are comulitave over all children,
                    // while ChildrenRelations[id] is per child. 
                    else
                    {
                        foreach (string obj in kvp.Value)
                        {
                            CN.ContextualizedQuestions[kvp.Key].Add(obj);
                            if (!URI_PreferdName.ContainsKey(obj))
                                URI_PreferdName.Add(obj, APIHandler.GetBioPortalClass(obj).PrefLabel);

                            // add the question with falsed checked (not selected to be updated later
                            if (!CN.AnswersChecked.ContainsKey(obj))
                                CN.AnswersChecked.Add(obj, false);

                            // count the number of times an answer/object attend to children
                            if (CN.AnswersCount.ContainsKey(obj))
                                CN.AnswersCount[obj]++;
                            else
                                CN.AnswersCount.Add(obj, 1);
                        }
                    }
                }
            }
        }
        private void BuildQuestionSetControls()
        {
            pnlContextQuestions.Controls.Clear();
            //int numOfQus = CN.QuestionsCount.Count();
            //int numOfAns = CN.AnswersCount.Count();
            List<Label> lblQuestion = new List<Label>();
            List<CheckBox> cbAnswers = new List<CheckBox>();
            List<CheckBox> cbNAAnswers = new List<CheckBox>();
            int i_q = 0;
            int i_a = 0;
            int i_na_a = 0;
            int H = 18;
            bool manditoryQuestion = false;

            int Qlength = CN.QuestionsCount.Keys.Max(l => l.Length);
            int Alength = CN.AnswersCount.Keys.Max(l => l.Length);

            foreach (KeyValuePair<string, HashSet<string>> QASet in CN.ContextualizedQuestions)
            {
                manditoryQuestion = false;
                Label label = new Label
                {
                    Name = "lbl_" + QASet.Key,
                    Text = QASet.Key.Substring(QASet.Key.IndexOf(@"SNOMEDCT/") + 9).Replace("has_", "").ToUpper(),
                    Width = Qlength,
                    AutoSize = true,
                    Location = new Point(5, 10 + ((i_a + i_q + i_na_a) * H)),
                };
                lblQuestion.Add(label);
                pnlContextQuestions.Controls.Add(lblQuestion[i_q]);
                i_q++;

                //Indicate it is a manditory question since all childer use this question
                //if (CN.QuestionsCount[QASet.Key] == CN.ChildrenScores.Count)
                //    td.Controls.Add(new HtmlGenericControl("span")
                //    {
                //        InnerText = "*"
                //    });

                foreach (string ASet in QASet.Value.OrderByDescending(o => o))
                {
                    string val = URI_PreferdName[ASet];
                    //cbAnswers[i_a] = new CheckBox
                    CheckBox checkBox = new CheckBox
                    {
                        Checked = false,
                        Text = val,
                        Name = "cb_Ans_" + val,
                        Width = Alength,
                        AutoSize = true,
                        Location = new Point(55, 10 + (i_q * H) + ((i_a + i_na_a) * H))
                    };
                    cbAnswers.Add(checkBox);
                    // check if all suchildren have this propertie then 
                    //cb.Checked = true;
                    //cb.Enabled = false;
                    if (CN.AnswersCount[ASet] == CN.ChildrenScores.Count)
                    {
                        cbAnswers[i_a].Checked = true;
                        cbAnswers[i_a].Enabled = false;
                        CN.AnswersChecked[ASet] = true;
                        CN.QuestionsChecked[QASet.Key] = true;
                        manditoryQuestion = true;
                    }

                    cbAnswers[i_a].CheckedChanged += cb_Ans_CheckedChanged;

                    pnlContextQuestions.Controls.Add(cbAnswers[i_a]);
                    i_a++;
                }
                // Add the NA answer and manage it seperatelly 
                if (!manditoryQuestion)
                {
                    CheckBox naCheckBox = new CheckBox
                    {
                        Checked = false,
                        Text = "N/A",
                        Name = "cb_Ans_NA_" + QASet.Key,
                        Width = Alength,
                        AutoSize = true,
                        Location = new Point(55, 10 + ((i_q) * H) + ((i_a + i_na_a) * H))
                    };
                    cbNAAnswers.Add(naCheckBox);
                    cbNAAnswers[i_na_a].CheckedChanged += cb_Ans_CheckedChanged;
                    pnlContextQuestions.Controls.Add(cbNAAnswers[i_na_a]);
                    i_na_a++;
                }
            }
        }
        private void BuildChildrenListControls()
        {
            pnlChildren.Controls.Clear();
            List<LinkLabel> ll_childern = new List<LinkLabel>();
            List<LinkLabel> ll_expand = new List<LinkLabel>();
            int i_c = 0;
            int H = 18;
            int cLength = CN.ChildrenScores.Keys.Max(l => l.Length);
            foreach (KeyValuePair<string, double> child in CN.ChildrenScores.OrderByDescending(v => v.Value))
            {
                LinkLabel linkLabel1 = new LinkLabel
                {
                    Text = $"({child.Value.ToString("F2")})_" + URI_PreferdName[child.Key],
                    Name = child.Key,
                    Width = cLength,
                    AutoSize = true,
                    Location = new Point(25, 10 + (i_c * H))
                };
                ll_childern.Add(linkLabel1);
                ll_childern[i_c].LinkClicked += lnklbl_LinkClicked;
                ll_childern[i_c].LinkBehavior = LinkBehavior.NeverUnderline;

                if (child.Value < CN.Threshold)
                    ll_childern[i_c].Enabled = false;
                else
                {
                    ll_childern[i_c].Enabled = true;
                    // Check if child.Key has children to expan
                    bool expandable = APIHandler.GetBioPortalChildrenCount(child.Key) > 0;

                    // Add the Expand linklabel
                    LinkLabel linkLabel2 = new LinkLabel
                    {
                        Image = expandable ? Properties.Resources.exp : Properties.Resources.notExp,
                        Text = " ",// expandable ? ">>" : "--",
                        Cursor = expandable ? Cursors.Hand : Cursors.No,
                        Name = child.Key,
                        Width = 13,
                        Height = H,
                        AutoSize = true,

                        Location = new Point(10, 10 + (i_c * H))
                    };
                    ll_expand.Add(linkLabel2);
                    ll_expand[i_c].LinkBehavior = LinkBehavior.NeverUnderline;
                    if (expandable)
                        ll_expand[i_c].Click += lnklblExpand_Clicked;


                    pnlChildren.Controls.Add(ll_expand[i_c]);
                }
                pnlChildren.Controls.Add(ll_childern[i_c]);
                i_c++;
            }
        }

        /// <summary>
        /// Updates the stats fiels
        /// </summary>
        private void UpdateStats()
        {
            ItrStats[Itr] = new Stats
            {
                SCT_ID_Init = $"{CN.ID.Replace("http://purl.bioontology.org/ontology/SNOMEDCT/", "")} | {URI_PreferdName[CN.ID]}",
                Iteration = Itr,
                Threshold = CN.Threshold,
                NumOfChildren = CN.ChildrenRelations.Keys.Count,
                NumOfCandidateChildren = CN.ChildrenScores.Count(c => c.Value >= CN.Threshold)
            };
            ItrStats[Itr].PecentOfReduction = 1 - ((double)ItrStats[Itr].NumOfCandidateChildren / (double)ItrStats[Itr].NumOfChildren);
            lblIteration.Text = ItrStats[Itr].Iteration.ToString();
            lblNumOfChildren.Text = ItrStats[Itr].NumOfChildren.ToString();
            lblNumOfCandidates.Text = ItrStats[Itr].NumOfCandidateChildren.ToString();
            lblReductionPercent.Text = ItrStats[Itr].PecentOfReduction.ToString("P");
        }
        private void UpdateChildrenScores()
        {
            foreach (KeyValuePair<string, Dictionary<string, HashSet<string>>> child in CN.ChildrenRelations)
            {
                double TP = 0; // Number of answers exist in the child's relations and is selected.
                double FP = 0; // Number of answers exist in the child's relations but not selected.
                double FN = 0; // Number of answers do not exist in the child's relations but selected.
                double P_base = 0; // is the precision base P_base = TP+FP (the number of answer exist in the child)
                double R_base = 0; // is the recall base R_base = TP+FN (the number of selected)

                double P, R, F1;
                foreach (KeyValuePair<string, HashSet<string>> RelObj in child.Value)
                {
                    foreach (string ans in RelObj.Value)
                        if (CN.AnswersChecked[ans])
                            TP++;
                    P_base += RelObj.Value.Count;
                }
                R_base = CN.AnswersChecked.Count(V => V.Value);
                P = TP / P_base;
                R = TP / R_base;
                F1 = (P + R) == 0 ? 0 : 2 * (P * R) / (P + R);
                CN.ChildrenScores[child.Key] = F1;
            }
        }

        private void UpdateThreshold()
        {
            double numOfSelectedOptions = 0;
            double numOfNonSelectedRelations = 0;
            // Reset the questions dictionary count 
            for (int i = 0; i < CN.QuestionsChecked.Count; i++)
            {
                string k = CN.QuestionsChecked.Keys.ElementAt(i);
                CN.QuestionsChecked[k] = false;
            }
            foreach (Control cntl in pnlContextQuestions.Controls)
            {
                if (cntl is CheckBox cb_Answers &&
                    cntl.Name.StartsWith("cb_Ans_") &&
                    !cntl.Name.StartsWith("cb_Ans_NA_") &&
                    cb_Answers.Checked)
                {
                    numOfSelectedOptions++;
                    string answerName = cntl.Name.Replace("cb_Ans_", "");
                    string answerURI = URI_PreferdName.FirstOrDefault(v => v.Value == answerName).Key;
                    string question = CN.ContextualizedQuestions.FirstOrDefault(vl => vl.Value.Contains(answerURI)).Key;
                    CN.QuestionsChecked[question] = true;
                }
            }

            // check if an additional NA was selected with other option, 
            // if an NA was selected with no other option we will not include it 
            foreach (Control cntl in pnlContextQuestions.Controls)
            {
                if (cntl is CheckBox cb_Answers &&
                    cntl.Name.StartsWith("cb_Ans_NA_") &&
                    cb_Answers.Checked)
                {
                    string question = cntl.Name.Replace("cb_Ans_NA_", "");
                    if (CN.QuestionsChecked[question])
                        numOfSelectedOptions++;
                }
            }

            numOfNonSelectedRelations = CN.QuestionsChecked.Count(T => !T.Value);
            CN.Threshold = numOfSelectedOptions / (numOfSelectedOptions + numOfNonSelectedRelations);
            lblTH.Text = CN.Threshold.ToString("F3");
        }


        private void btnSCTQuery_Click(object sender, EventArgs e)
        {
            // reset the search and start a new search.
            if (txtSCTQuery.Text.Trim().Equals(string.Empty))
                MessageBox.Show("Please enter a valid SNOMED CT ID or Query Term.");
            else
            {
                InitNewSearch();
                Search(txtSCTQuery.Text.Trim());
                if (CN.ChildrenRelations.Count > 0)
                {
                    BuildQuestionSetControls();
                    UpdateThreshold();
                }
                btnShowCandIDs.Enabled = true;
            }
        }
        private void cb_Ans_CheckedChanged(object sender, EventArgs e)
        {
            if (!((CheckBox)sender).Name.StartsWith("cb_Ans_NA_"))
            {
                string answerName = ((CheckBox)sender).Name.Replace("cb_Ans_", "");
                string answerURI = URI_PreferdName.FirstOrDefault(v => v.Value == answerName).Key;
                CN.AnswersChecked[answerURI] = ((CheckBox)sender).Checked;
            }
            UpdateThreshold();
        }
        private void lnklblExpand_Clicked(object sender, EventArgs e)
        {
            btnShowCandIDs.Enabled = false;
            pnlContextQuestions.Controls.Clear();
            pnlChildren.Controls.Clear();
            ExpandChild(((LinkLabel)sender).Name);
            BuildQuestionSetControls();
            UpdateThreshold();
            btnShowCandIDs.Enabled = true;
        }
        private void lnklbl_LinkClicked(object sender, EventArgs e)
        {
            string id = ((LinkLabel)sender).Name.Substring(((LinkLabel)sender).Name.LastIndexOf(@"SNOMEDCT/") + 9);
            string name = URI_PreferdName[((LinkLabel)sender).Name];
            if (MessageBox.Show($"Is the SNOMED CT Term \n\n\t({id} | {name})\n\n\t the correct term? ", "SNOMED CT Term Found", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                AddNewNode(id, name,Itr);
                btnSaveStates.Enabled = true;
                ItrStats[0].SCT_ID_Target = $"{id} | {name}";
            }
        }

        private void btnShowCandIDs_Click(object sender, EventArgs e)
        {
            UpdateThreshold();
            UpdateChildrenScores();
            BuildChildrenListControls();
            UpdateStats();
        }

        private void btnSaveStates_Click(object sender, EventArgs e)
        {
            // Use this line if there are more than one iterations and you are printing the initial term not its parent
            // as the initial development was to manually find the parent and use it as the initial search term. 
            ItrStats[0].SaveCustomHeader((ItrStats.Count==1 ? ItrStats[0]: ItrStats[1]).SCT_ID_Init);
            //ItrStats[0].SaveCustomHeader(ItrStats[0].SCT_ID_Init);
            foreach (KeyValuePair<int, Stats> itr in ItrStats)
            {
                itr.Value.Save();
            }
            InitNewSearch();
        }
    }

    public class Stats
    {
        public string SCT_ID_Init { get; set; }
        public string SCT_ID_Target { get; set; }
        public int Iteration { get; set; }
        public double Threshold { get; set; }
        public int NumOfChildren { get; set; }
        public int NumOfCandidateChildren { get; set; }
        public double PecentOfReduction { get; set; }

        public void SaveCustomHeader(string init, string target = null)
        {
            var csv = new StringBuilder();
            string newLine = string.Format("{0},,,{1}", init, target ?? SCT_ID_Target);
            csv.AppendLine(newLine);
            newLine = string.Format("Iteration #,Threshold,# SCT children,# of Candidate SCT,% of search-space reduction");
            csv.AppendLine(newLine);

            File.AppendAllText("./Stats.csv", csv.ToString());
        }
        public void SaveHeader()
        {
            var csv = new StringBuilder();
            string newLine = string.Format("{0},,,{1}", SCT_ID_Init, SCT_ID_Target);
            csv.AppendLine(newLine);
            newLine = string.Format("Iteration #,Threshold,# SCT children,# of Candidate SCT,% of search-space reduction");
            csv.AppendLine(newLine);

            File.AppendAllText("./Stats.csv", csv.ToString());
        }
        public void Save()
        {
            var csv = new StringBuilder();
            string newLine = string.Format("{0},{1},{2},{3},{4}", Iteration, Threshold, NumOfChildren, NumOfCandidateChildren, PecentOfReduction);
            csv.AppendLine(newLine);

            File.AppendAllText("./Stats.csv", csv.ToString());
        }
    }
    public class CTNode
    {
        public CTNode()
        {
            ContextualizedQuestions = new Dictionary<string, HashSet<string>>();
            ChildrenScores = new Dictionary<string, double>();
            ChildrenRelations = new Dictionary<string, Dictionary<string, HashSet<string>>>();
            QuestionsCount = new Dictionary<string, int>();
            AnswersCount = new Dictionary<string, int>();
            AnswersChecked = new Dictionary<string, bool>();
            QuestionsChecked = new Dictionary<string, bool>();
            ID = string.Empty;
            Threshold = 0;
        }
        /// <summary>
        /// A dictionary of the list of relations (questions) and their possible objects (answers)
        /// { relationID, List of objectsIDs }
        /// </summary>
        public Dictionary<string, HashSet<string>> ContextualizedQuestions;
        /// <summary>
        /// tracks the children and their score based on thier relations.
        /// { childID, Score }
        /// </summary>
        public Dictionary<string, double> ChildrenScores;
        /// <summary>
        /// tracks the relation:object set for each child
        /// { childID, { relationID, List of objectsIDs } }
        /// </summary>
        public Dictionary<string, Dictionary<string, HashSet<string>>> ChildrenRelations;
        /// <summary>
        /// tracks the count for each question to set manditory question.
        /// { relationID, count it appeared in children}
        /// </summary>
        public Dictionary<string, int> QuestionsCount;
        /// <summary>
        /// tracks the count for each answer to set manditory unchangable answer (disabled control)
        /// { ObjectID, count it appeared in children}
        /// </summary>
        public Dictionary<string, int> AnswersCount;
        /// <summary>
        /// track the checked questions and answers for the current node.
        /// </summary>
        public Dictionary<string, bool> AnswersChecked;
        public Dictionary<string, bool> QuestionsChecked;
        /// <summary>
        /// The Threshold value based on the anwer of the Contextualized Questions.
        /// </summary>
        public double Threshold;
        /// <summary>
        /// Holds a string value of the current processed SNOMED CT ID
        /// i.g. @"http://purl.bioontology.org/ontology/SNOMEDCT/230716006"
        /// </summary>
        public string ID;

        //public static CTNode FromJson(string json) => JsonConvert.DeserializeObject<CTNode>(json, Misc.Converter.Settings);
    }
}

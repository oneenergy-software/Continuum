using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ContinuumNS
{
    public partial class SelectMetsForDebiasCalc : Form
    {
        public MetCollection metList;
        public double mcpHeight;
        public string selMetNamesCommaStr;
        public string selMetName;

        public bool goodToGo = false;


        public SelectMetsForDebiasCalc(MetCollection metCollection, string selectedMetName, double mcpH, string selMetsCommaStr)
        {
            InitializeComponent();
            ContinuumStyle.ApplyStyles(this);
            metList = metCollection;
            selMetName = selectedMetName;
            mcpHeight = mcpH;
            selMetNamesCommaStr = selMetsCommaStr;
            UpdateMetTable();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        /// <summary>
        /// Update table to show met name, slope, intercept, and R^2 of all except the selected met name
        /// </summary>
        public void UpdateMetTable()
        {
            string[] selectedMets = selMetNamesCommaStr.Split(',');

            for (int m = 0; m < metList.ThisCount; m++)
            {
                if (metList.metItem[m].name != selMetName)
                {
                    int rowInd = dataMetsForDebias.Rows.Add(metList.metItem[m].name, false);
                    dataMetsForDebias.Rows[rowInd].Cells[0].Value = metList.metItem[m].name;

                    MCP thisMCP = metList.metItem[m].GetMCP_ByHeight(mcpHeight);

                    if (selectedMets.Contains(metList.metItem[m].name))                    
                        dataMetsForDebias.Rows[rowInd].Cells[0].Value = true;                    
                    else
                        dataMetsForDebias.Rows[rowInd].Cells[0].Value = false;

                    dataMetsForDebias.Rows[rowInd].Cells[1].Value = metList.metItem[m].name;
                    dataMetsForDebias.Rows[rowInd].Cells[2].Value = Math.Round(thisMCP.debiasData.slope, 4);
                    dataMetsForDebias.Rows[rowInd].Cells[3].Value = Math.Round(thisMCP.debiasData.intercept, 4);
                    dataMetsForDebias.Rows[rowInd].Cells[4].Value = Math.Round(thisMCP.debiasData.R_sq, 4);
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            // Make sure at least one met is selected, create new string of selected met names, and return to calling form
            List<string> selectedMets = new List<string>();

            for (int r = 0; r < dataMetsForDebias.Rows.Count; r++)
                if (Convert.ToBoolean(dataMetsForDebias.Rows[r].Cells[0].Value) == true)
                    selectedMets.Add(dataMetsForDebias.Rows[r].Cells[1].Value.ToString());

            if (selectedMets.Count == 0)
            {
                MessageBox.Show("You must select at least one met to use for the debias calculation.");
                return;
            }
            else
            {
                selMetNamesCommaStr = string.Join(",", selectedMets);
                goodToGo = true;
                Close();
            }
        }
    }
}

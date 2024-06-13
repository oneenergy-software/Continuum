using System;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ContinuumNS
{
    /// <summary> GUI class that provides a tool to generate met data headers for time series data import. </summary>
    public partial class GenHeaders : Form
    {
        /// <summary> GUI class initializer. </summary>
        FolderBrowserDialog fbd = new FolderBrowserDialog(); // FolderBrowserDialog to return path to StreamWriter for saved file path
        int checkCountTemp = 0; // Counting variable to ensure data is included for Temperature i.e.(Avg, SD, Min, Max)
        int checkCountVane = 0; // Counting variable to ensure data is included for Vanes i.e.(Avg, SD, Min, Max)
        int checkCountAnem = 0; // Counting variable to ensure data is included for Anemometers i.e.(Avg, SD, Min, Max)
        bool successful = false; // boolean for formatting successful or not

        /// <summary> GUI class initializer. </summary>
        public GenHeaders()
        {
            InitializeComponent();
            // Centers the text of specified textboxes
            txtNewAnemHeight.TextAlign = HorizontalAlignment.Center;
            txtNewAnemOrient.TextAlign = HorizontalAlignment.Center;
            txtNewVaneHeight.TextAlign = HorizontalAlignment.Center;
            txtNewTempHeight.TextAlign = HorizontalAlignment.Center;
            txtFileName.TextAlign = HorizontalAlignment.Center;
            // Formats the comboboxes to have drop-down units for the user to select
            string[] units = new string[] { "mps", "mph" };
            cboWindSpeedUnits.Items.AddRange(units);
            string[] temps = new string[] { "C", "F" };
            cboTempUnits.Items.AddRange(temps);
            // Sets tab path from each textbox to the next
            txtMETname.TabIndex = 1;
            txtLat.TabIndex = 2;
            txtLong.TabIndex = 3;
            cboWindSpeedUnits.TabIndex = 4;
            txtNewAnemHeight.TabIndex = 5;
            txtNewAnemOrient.TabIndex = 6;
            txtNewVaneHeight.TabIndex = 7;
            txtNewTempHeight.TabIndex = 8;
            txtFileName.TabIndex = 9;

        }                    

        private void txtLat_TextChanged(object sender, EventArgs e)
        {
            // Gets latitude and converts the string to an array of ASCII values and makes sure the input is only numbers, a negative sign, or a decimal point
            string message = "";
            string lat = txtLat.Text;
            byte[] bytes = Encoding.ASCII.GetBytes(lat);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] != 45 && bytes[i] != 46 && (bytes[i] < 48 || bytes[i] > 57))
                    message = "Input must be a number.";
            }

            if (!String.IsNullOrWhiteSpace(message))
                MessageBox.Show(this, message);

        }

        private void txtLong_TextChanged(object sender, EventArgs e)
        {
            // Gets longitude and converts the string to an array of ASCII values and makes sure the input is only numbers, a negative sign, or a decimal point
            string message = "";
            string longg = txtLong.Text;
            byte[] bytes = Encoding.ASCII.GetBytes(longg);
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] != 45 && bytes[i] != 46 && (bytes[i] < 48 || bytes[i] > 57))
                    message = "Input must be a number.";
            }

            if (!String.IsNullOrWhiteSpace(message))
                MessageBox.Show(this, message);

        }        
               
        private void btnAddTempHeight_Click(object sender, EventArgs e)
        {
            bool successful = false; 

            // Adds a height to the temperature box when "Add" button is clicked as long as it has text in the height box
            if (!string.IsNullOrWhiteSpace(txtNewTempHeight.Text))
            {
                // Trys to turn the text into an integer and catches invalid input
                try
                {
                    Int32.Parse(txtNewTempHeight.Text);
                    successful = true;
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Invalid input.");
                    txtNewTempHeight.Clear();
                }
            }

            // If valid input, adds text to text box
            if (successful == true)
            {
                // Makes a new line right before the text to help with readability and helps ensure remove button works
                listTempsHeight.Items.Add(txtNewTempHeight.Text);
                txtNewTempHeight.Clear(); // Resets text box to empty for ease of use for user
            }

        }

        private void btnAddAnem_Click(object sender, EventArgs e)
        {
            bool successful = false; // Boolean for integer parse

            // Adds a height to the anemometer box when "Add" button is clicked as long as it has text in the height and orientation box
            if (!string.IsNullOrWhiteSpace(txtNewAnemHeight.Text) && !string.IsNullOrWhiteSpace(txtNewAnemOrient.Text))
            {
                // Trys to turn the text into an integer and catches invalid input
                try
                {
                    Int32.Parse(txtNewAnemHeight.Text); Int32.Parse(txtNewAnemOrient.Text);
                    successful = true;
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Invalid input.");
                    txtNewAnemHeight.Clear(); txtNewAnemOrient.Clear();
                }
            }
            // Prevents error to be thrown by program and lets user know BOTH height and orientation have to be entered for anemometer data
            else
                MessageBox.Show(this, "Please enter both height and orientation.");

            // If valid input, adds text to text box
            if (successful == true)
            {
                // Makes a new line right before the text to help with readability and helps ensure remove button works
                listAnemsHO.Items.Add(txtNewAnemHeight.Text + "_" + txtNewAnemOrient.Text);
                txtNewAnemHeight.Clear(); txtNewAnemOrient.Clear(); // Resets text boxes to empty for ease of use for user
            }

        }

        private void btnAddVane_Click(object sender, EventArgs e)
        {
            bool successful = false; // Boolean for integer parse

            // Adds a height to the vane box when "Add" button is clicked as long as it has text in the height box
            if (!string.IsNullOrWhiteSpace(txtNewVaneHeight.Text))
            {
                // Trys to turn the text into an integer and catches invalid input
                try
                {
                    Int32.Parse(txtNewVaneHeight.Text);
                    successful = true;
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Invalid input.");
                    txtNewVaneHeight.Clear();
                }
            }

            // If valid input, adds text to text box
            if (successful == true)
            {
                // Makes a new line right before the text to help with readability and helps ensure remove button works
                listVanesHeight.Items.Add(txtNewVaneHeight.Text);
                txtNewVaneHeight.Clear(); // Resets text box to empty for ease of use for user
            }

        }
        
        private void btnRemoveAnemHeight_Click(object sender, EventArgs e)
        {
            // Gets index of selected text and removes the data at that index
            if (listAnemsHO.SelectedIndex != -1)
                listAnemsHO.Items.RemoveAt(listAnemsHO.SelectedIndex);
            else
                MessageBox.Show("Please selected data to be deleted.");

        }
        
        private void btnRemoveVaneHeight_Click(object sender, EventArgs e)
        {
            // Gets index of selected text and removes the data at that index
            if (listVanesHeight.SelectedIndex != -1)
                listVanesHeight.Items.RemoveAt(listVanesHeight.SelectedIndex);
            else
                MessageBox.Show("Please selected data to be deleted.");

        }

        private void btnRemoveTempHeight_Click(object sender, EventArgs e)
        {
            // Gets index of selected text and removes the data at that index
            if (listTempsHeight.SelectedIndex != -1)
                listTempsHeight.Items.RemoveAt(listTempsHeight.SelectedIndex);
            else
                MessageBox.Show("Please selected data to be deleted.");

        }                       

        private void btnFormat_Click(object sender, EventArgs e)
        {
            string message = ""; // Initializes variable for error message

            // Checks if MET Tower name was entered or not and adds error to message
            if (string.IsNullOrWhiteSpace(txtMETname.Text))
                message += "Please enter MET tower name.\n";

            // Checks if temperature units are selected and adds error to message
            if (cboTempUnits.SelectedIndex == -1 & listTempsHeight.Items.Count > 0)
                message += "Please select temperature units.\n";

            // Checks if wind speed units are selected and adds error to message
            if (cboWindSpeedUnits.SelectedIndex == -1)
                message += "Please select wind speed units.\n";

            // Checks if latitiude is entered and adds error to message
            if (string.IsNullOrWhiteSpace(txtLat.Text))
                message += "Please enter latitude.\n";

            // Checks if longitude is entered and adds error to message
            if (string.IsNullOrWhiteSpace(txtLong.Text))
                message += "Please enter longitude.\n";
                   
            // Checks if anemometer data has been entered and adds error to message
            if (listAnemsHO.Items.Count == 0)
                message += "Please enter anemometer height.\n";

            // Checks if vane data has been entered and adds error to message
            if (listVanesHeight.Items.Count == 0)
                message += "Please enter vane height.\n";
                   
            // Checks if file name has been given and adds error to message
            if (string.IsNullOrWhiteSpace(txtFileName.Text))
                message += "Please enter a file name.\n";

            // Shows dialog box with any errors that are in message
            if (!string.IsNullOrWhiteSpace(message))
                MessageBox.Show(this, message);

            // There are no errors and the formating of the .csv file can begin
            else
            {
                // Calls function that allows user to select where the file will be stored
                FolderSelected();

                if (successful)
                {
                    StreamWriter sm = new StreamWriter(fbd.SelectedPath + "\\" + txtFileName.Text + ".csv");

                    // Formats and writes top two lines to the .csv file as shown in example at the beginning of the code
                    sm.WriteLine(txtMETname.Text + ",");
                    sm.WriteLine("WS Units," + cboWindSpeedUnits.Text + ",Lat," + txtLat.Text + ",Long," + txtLong.Text + ",");
                    sm.Write("Date & Time Stamp,");

                    // Converts the text in the anemometor, vane, temperature, and pressure text boxes into string arrays
                    string[] AnemString = CreateArrayAnem();
                    string[] VaneString = CreateArrayVane();
                    string[] TempString = CreateArrayTemp();
                    string[] baroString = CreateArrayBaro();

                    // For loop that loops through the anemometor data and adds the correct suffixes based on what data the user is including in the .csv file
                    for (int i = 0; i < AnemString.Length; i++)
                    {
                        if (chkAvgAnem.Checked == true)
                            sm.Write(AnemString[i] + "_Avg,");
                        if (chkSDAnem.Checked == true)
                            sm.Write(AnemString[i] + "_SD,");
                        if (chkMaxAnem.Checked == true)
                            sm.Write(AnemString[i] + "_Max,");
                        if (chkMinAnem.Checked == true)
                            sm.Write(AnemString[i] + "_Min,");

                    }

                    // For loop that loops through the vane data and adds the correct suffixes based on what data the user is including in the .csv file
                    for (int i = 0; i < VaneString.Length; i++)
                    {
                        if (chkAvgVane.Checked == true)
                            sm.Write(VaneString[i] + "_Avg,");
                        if (chkSDVane.Checked == true)
                            sm.Write(VaneString[i] + "_SD,");
                        if (chkMaxVane.Checked == true)
                            sm.Write(VaneString[i] + "_Max,");
                        if (chkMinVane.Checked == true)
                            sm.Write(VaneString[i] + "_Min,");

                    }

                    // For loop that loops through the temperature data and adds the correct suffixes based on what data the user is including in the .csv file
                    for (int i = 0; i < TempString.Length; i++)
                    {
                        if (chkAvgTemp.Checked == true)
                            sm.Write(TempString[i] + "_Avg_" + cboTempUnits.Text + ",");
                        if (chkSDTemp.Checked == true)
                            sm.Write(TempString[i] + "_SD_" + cboTempUnits.Text + ",");
                        if (chkMaxTemp.Checked == true)
                            sm.Write(TempString[i] + "_Max_" + cboTempUnits.Text + ",");
                        if (chkMinTemp.Checked == true)
                            sm.Write(TempString[i] + "_Min_" + cboTempUnits.Text + ",");
                    }

                    for (int i = 0; i < baroString.Length; i++)
                    {
                        if (chkAvgBaro.Checked == true)
                            sm.Write(baroString[i] + "_Avg,");
                        if (chkSDBaro.Checked == true)
                            sm.Write(baroString[i] + "_SD,");
                        if (chkMaxBaro.Checked == true)
                            sm.Write(baroString[i] + "_Max,");
                        if (chkMinBaro.Checked == true)
                            sm.Write(baroString[i] + "_Min,");
                    }

                    sm.Close(); // Closes StreamWriter for file to be completley saved
                    if (successful == true) MessageBox.Show(this, "Successful!"); // Let's user know the file has been saved successfully
                    else MessageBox.Show(this, "Unsuccessful."); // Let's user know tht=e file has not been saved
                }
            }
            
        }

        /// <summary> Creates a string array from anemometer data </summary>         
        public string[] CreateArrayAnem()
        {
            // Makes new string length of data entered into temperature box
            string[] ArrayHeight = new string[listAnemsHO.Items.Count];

            // Formats string array for Continuum
            for (int i = 0; i < listAnemsHO.Items.Count; i++)            
                ArrayHeight[i] = "Anem_" + listAnemsHO.Items[i].ToString();            

            return ArrayHeight;

        }

        /// <summary> Creates a string array from vane data. </summary>        
        public string[] CreateArrayVane()
        {
            // Makes new string length of data entered into vane box
            string[] ArrayHeight = new string[listVanesHeight.Items.Count];

            // Formats string array for Continuum 
            for (int i = 0; i < listVanesHeight.Items.Count; i++)            
                ArrayHeight[i] = "Vane_" + listVanesHeight.Items[i].ToString();            

            return ArrayHeight;

        }

        /// <summary> Creates a string array from temperature data </summary>        
        public string[] CreateArrayTemp()
        {
            // Makes new string length of data entered into temperature box
            string[] ArrayHeight = new string[listTempsHeight.Items.Count];

            // Formats string array for Continuum 
            for (int i = 0; i < listTempsHeight.Items.Count; i++)            
                ArrayHeight[i] = "Temp_" + listTempsHeight.Items[i].ToString();            

            return ArrayHeight;

        }

        /// <summary> Creates a string array from pressure data </summary>        
        public string[] CreateArrayBaro()
        {
            // Makes new string length of data entered into pressure box
            string[] ArrayHeight = new string[listBaroHeight.Items.Count];

            // Formats string array for Continuum 
            for (int i = 0; i < listBaroHeight.Items.Count; i++)
                ArrayHeight[i] = "Baro_" + listBaroHeight.Items[i].ToString();

            return ArrayHeight;

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clears the app of any data if user wants to start over
            txtLat.Clear();
            txtLong.Clear();
            txtMETname.Clear();
            txtFileName.Clear();
            txtNewAnemHeight.Clear();
            txtNewAnemOrient.Clear();
            txtNewVaneHeight.Clear();
            txtNewTempHeight.Clear();
            listAnemsHO.Items.Clear();
            chkSDTemp.Checked = false;
            chkAvgTemp.Checked = false;
            chkMinTemp.Checked = false;
            chkMaxTemp.Checked = false;
            chkSDVane.Checked = false;
            chkAvgVane.Checked = false;
            chkMinVane.Checked = false;
            chkMaxVane.Checked = false;
            chkSDAnem.Checked = false;
            chkAvgAnem.Checked = false;
            chkMinAnem.Checked = false;
            chkMaxAnem.Checked = false;
            
            listVanesHeight.Items.Clear();
            listTempsHeight.Items.Clear();
            

        }

        
        /// <summary> Prompts user to select folder for saving the .csv file and saves the path for StreamWriter </summary>        
        public void FolderSelected()
        {
            // Sets default folder to desktop and adds text to tell user to select folder for where the .csv is to be saved
            fbd.RootFolder = Environment.SpecialFolder.Desktop;
            fbd.Description = "+++Select Folder to Save File in+++";
            fbd.ShowNewFolderButton = false;

            // Prompts user to select folder and click "OK" and catches any exception thrown by the program
            try
            {
                if (fbd.ShowDialog() == DialogResult.OK)
                {
                    successful = true;
                    return;
                }
                else
                {
                    successful = false;
                    return;
                }
            }
            catch
            {
                MessageBox.Show(this, "Folder path not valid.", "", MessageBoxButtons.OK);
            }
        }

        private void chkAvgVane_CheckedChanged(object sender, EventArgs e)
        {
            // Increments/Decrements checkCount by one if data is selected/unselected
            if (chkAvgVane.Checked == true)
                checkCountVane++;
            else
                checkCountVane--;

        }

        private void chkSDVane_CheckedChanged(object sender, EventArgs e)
        {
            // Increments/Decrements checkCount by one if data is selected/unselected
            if (chkSDVane.Checked == true)
                checkCountVane++;
            else
                checkCountVane--;

        }

        private void chkMinVane_CheckedChanged(object sender, EventArgs e)
        {
            // Increments/Decrements checkCount by one if data is selected/unselected
            if (chkMinVane.Checked == true)
                checkCountVane++;
            else
                checkCountVane--;

        }

        private void chkMaxVane_CheckedChanged(object sender, EventArgs e)
        {
            // Increments/Decrements checkCount by one if data is selected/unselected
            if (chkMaxVane.Checked == true)
                checkCountVane++;
            else
                checkCountVane--;

        }

        private void chkAvgAnem_CheckedChanged(object sender, EventArgs e)
        {
            // Increments/Decrements checkCount by one if data is selected/unselected
            if (chkAvgAnem.Checked == true)
                checkCountAnem++;
            else
                checkCountAnem--;

        }

        private void chkSDAnem_CheckedChanged(object sender, EventArgs e)
        {
            // Increments/Decrements checkCount by one if data is selected/unselected
            if (chkSDAnem.Checked == true)
                checkCountAnem++;
            else
                checkCountAnem--;

        }

        private void chkMinAnem_CheckedChanged(object sender, EventArgs e)
        {
            // Increments/Decrements checkCount by one if data is selected/unselected
            if (chkMinAnem.Checked == true)
                checkCountAnem++;
            else
                checkCountAnem--;

        }

        private void chkMaxAnem_CheckedChanged(object sender, EventArgs e)
        {
            // Increments/Decrements checkCount by one if data is selected/unselected
            if (chkMaxAnem.Checked == true)
                checkCountAnem++;
            else
                checkCountAnem--;

        }

        private void btnAddBaroHeight_Click(object sender, EventArgs e)
        {
            bool successful = false;

            // Adds a height to the pressure (barometer) box when "Add" button is clicked as long as it has text in the height box
            if (!string.IsNullOrWhiteSpace(txtNewBaroHeight.Text))
            {
                // Trys to turn the text into an integer and catches invalid input
                try
                {
                    double.Parse(txtNewBaroHeight.Text);
                    successful = true;
                }
                catch (Exception)
                {
                    MessageBox.Show(this, "Invalid input.");
                    txtNewBaroHeight.Clear();
                }
            }

            // If valid input, adds text to text box
            if (successful == true)
            {
                // Makes a new line right before the text to help with readability and helps ensure remove button works
                listBaroHeight.Items.Add(txtNewBaroHeight.Text);
                txtNewBaroHeight.Clear(); // Resets text box to empty for ease of use for user
            }
        }

        private void btnRemoveBaroHeight_Click(object sender, EventArgs e)
        {
            // Gets index of selected text and removes the data at that index
            if (listBaroHeight.SelectedIndex != -1)
                listBaroHeight.Items.RemoveAt(listBaroHeight.SelectedIndex);
            else
                MessageBox.Show("Please selected data to be deleted.");
        }
    }
}
using System;
using System.Windows.Forms;
using MobileStockApp.Database;
using MobileStockApp.Models;

namespace MobileStockApp.Forms
{
    /// <summary>
    /// Main form for the Mobile Stock Capture application
    /// SECTION C.1: User Interface Design (10 Marks)
    /// </summary>
    public partial class MainForm : Form
    {
        private DatabaseManager _dbManager;
        private TextBox txtCode;
        private TextBox txtMake;
        private TextBox txtQuantity;
        private Label lblOutput;
        private DataGridView dgvRecords;

        public MainForm()
        {
            InitializeComponent();
            _dbManager = new DatabaseManager();
        }

        /// <summary>
        /// Initialize UI components
        /// </summary>
        private void InitializeComponent()
        {
            this.Text = "Mobile Stock Capture Application";
            this.Size = new System.Drawing.Size(700, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.BackColor = System.Drawing.Color.LightGray;

            // Title Label
            Label lblTitle = new Label()
            {
                Text = "Mobile Stock Capture System",
                Location = new System.Drawing.Point(20, 10),
                Width = 650,
                Height = 30,
                Font = new System.Drawing.Font("Arial", 16, System.Drawing.FontStyle.Bold),
                ForeColor = System.Drawing.Color.DarkBlue
            };
            this.Controls.Add(lblTitle);

            // Mobile Code Label and TextBox
            Label lblCode = new Label()
            {
                Text = "Mobile Code:",
                Location = new System.Drawing.Point(20, 50),
                Width = 100
            };
            txtCode = new TextBox()
            {
                Name = "txtCode",
                Location = new System.Drawing.Point(130, 50),
                Width = 200
            };
            this.Controls.Add(lblCode);
            this.Controls.Add(txtCode);

            // Make Label and TextBox
            Label lblMake = new Label()
            {
                Text = "Make:",
                Location = new System.Drawing.Point(20, 90),
                Width = 100
            };
            txtMake = new TextBox()
            {
                Name = "txtMake",
                Location = new System.Drawing.Point(130, 90),
                Width = 200
            };
            this.Controls.Add(lblMake);
            this.Controls.Add(txtMake);

            // Quantity Label and TextBox
            Label lblQuantity = new Label()
            {
                Text = "Quantity:",
                Location = new System.Drawing.Point(20, 130),
                Width = 100
            };
            txtQuantity = new TextBox()
            {
                Name = "txtQuantity",
                Location = new System.Drawing.Point(130, 130),
                Width = 200
            };
            this.Controls.Add(lblQuantity);
            this.Controls.Add(txtQuantity);

            // Button Panel
            Panel buttonPanel = new Panel()
            {
                Location = new System.Drawing.Point(20, 170),
                Width = 650,
                Height = 50,
                BackColor = System.Drawing.Color.WhiteSmoke
            };

            // Add Button
            Button btnAdd = new Button()
            {
                Name = "btnAdd",
                Text = "Add",
                Location = new System.Drawing.Point(10, 10),
                Width = 80,
                Height = 30,
                BackColor = System.Drawing.Color.LimeGreen,
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            btnAdd.Click += BtnAdd_Click;
            buttonPanel.Controls.Add(btnAdd);

            // Delete Button
            Button btnDelete = new Button()
            {
                Name = "btnDelete",
                Text = "Delete",
                Location = new System.Drawing.Point(100, 10),
                Width = 80,
                Height = 30,
                BackColor = System.Drawing.Color.Red,
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            btnDelete.Click += BtnDelete_Click;
            buttonPanel.Controls.Add(btnDelete);

            // Find Button
            Button btnFind = new Button()
            {
                Name = "btnFind",
                Text = "Find",
                Location = new System.Drawing.Point(190, 10),
                Width = 80,
                Height = 30,
                BackColor = System.Drawing.Color.RoyalBlue,
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            btnFind.Click += BtnFind_Click;
            buttonPanel.Controls.Add(btnFind);

            // Clear Button
            Button btnClear = new Button()
            {
                Name = "btnClear",
                Text = "Clear",
                Location = new System.Drawing.Point(280, 10),
                Width = 80,
                Height = 30,
                BackColor = System.Drawing.Color.Orange,
                ForeColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            btnClear.Click += BtnClear_Click;
            buttonPanel.Controls.Add(btnClear);

            this.Controls.Add(buttonPanel);

            // Output Status Label
            Label lblStatusLabel = new Label()
            {
                Text = "Status:",
                Location = new System.Drawing.Point(20, 230),
                Width = 100,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(lblStatusLabel);

            lblOutput = new Label()
            {
                Name = "lblOutput",
                Location = new System.Drawing.Point(130, 230),
                Width = 540,
                Height = 40,
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = System.Drawing.Color.White,
                Font = new System.Drawing.Font("Arial", 10)
            };
            this.Controls.Add(lblOutput);

            // Data Grid Label
            Label lblGridLabel = new Label()
            {
                Text = "Records:",
                Location = new System.Drawing.Point(20, 280),
                Width = 100,
                Font = new System.Drawing.Font("Arial", 10, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(lblGridLabel);

            // Data Grid View
            dgvRecords = new DataGridView()
            {
                Name = "dgvRecords",
                Location = new System.Drawing.Point(20, 310),
                Width = 650,
                Height = 250,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false
            };
            this.Controls.Add(dgvRecords);

            RefreshDataGrid();
        }

        /// <summary>
        /// SECTION C.2.1: BtnAdd Click Event - Add new mobile phone record
        /// When the btnAdd button is clicked, the program should insert a record 
        /// into the tblMobilePhones with the MobileCode, Make and Quantity
        /// </summary>
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                // Validation
                if (string.IsNullOrWhiteSpace(txtCode.Text) ||
                    string.IsNullOrWhiteSpace(txtMake.Text) ||
                    string.IsNullOrWhiteSpace(txtQuantity.Text))
                {
                    lblOutput.Text = "Error: All fields required";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (!int.TryParse(txtQuantity.Text, out int quantity) || quantity < 0)
                {
                    lblOutput.Text = "Error: Quantity must be a valid positive number";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                MobilePhone phone = new MobilePhone(txtCode.Text.Trim(), txtMake.Text.Trim(), quantity);

                if (_dbManager.AddRecord(phone))
                {
                    lblOutput.Text = "Record Added";
                    lblOutput.ForeColor = System.Drawing.Color.Green;
                    ClearFields();
                    RefreshDataGrid();
                }
                else
                {
                    lblOutput.Text = "Error: Could not add record (may already exist)";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                }
            }
            catch (Exception ex)
            {
                lblOutput.Text = $"Error: {ex.Message}";
                lblOutput.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// SECTION C.2.2: BtnDelete Click Event - Delete mobile phone record by MobileCode
        /// When the btnDelete button is clicked, the program should delete the record 
        /// whose MobileCode is typed in the txtCode text field and display the message 
        /// "Record Found" in the lblOutput; or "Record NOT Found" if the record is not found
        /// </summary>
        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text))
                {
                    lblOutput.Text = "Error: Please enter Mobile Code";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                if (_dbManager.DeleteRecord(txtCode.Text.Trim()))
                {
                    lblOutput.Text = "Record Found";
                    lblOutput.ForeColor = System.Drawing.Color.Green;
                    ClearFields();
                    RefreshDataGrid();
                }
                else
                {
                    lblOutput.Text = "Record NOT Found";
                    lblOutput.ForeColor = System.Drawing.Color.Orange;
                }
            }
            catch (Exception ex)
            {
                lblOutput.Text = $"Error: {ex.Message}";
                lblOutput.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// SECTION C.2.3: BtnFind Click Event - Find and retrieve mobile phone record
        /// When the btnFind is clicked, the program should find the record whose
        /// MobileCode is typed in the txtCode text field and display the message "Record Deleted"
        /// </summary>
        private void BtnFind_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtCode.Text))
                {
                    lblOutput.Text = "Error: Please enter Mobile Code";
                    lblOutput.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                MobilePhone phone = _dbManager.FindRecord(txtCode.Text.Trim());

                if (phone != null)
                {
                    txtMake.Text = phone.Make;
                    txtQuantity.Text = phone.Quantity.ToString();
                    lblOutput.Text = "Record Found";
                    lblOutput.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblOutput.Text = "Record NOT Found";
                    lblOutput.ForeColor = System.Drawing.Color.Orange;
                }
            }
            catch (Exception ex)
            {
                lblOutput.Text = $"Error: {ex.Message}";
                lblOutput.ForeColor = System.Drawing.Color.Red;
            }
        }

        /// <summary>
        /// BtnClear Click Event - Clear all input fields
        /// </summary>
        private void BtnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
            lblOutput.Text = "";
        }

        /// <summary>
        /// Clear all input fields
        /// </summary>
        private void ClearFields()
        {
            txtCode.Text = "";
            txtMake.Text = "";
            txtQuantity.Text = "";
        }

        /// <summary>
        /// Refresh the data grid with all records
        /// </summary>
        private void RefreshDataGrid()
        {
            try
            {
                var records = _dbManager.GetAllRecords();
                dgvRecords.DataSource = records;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error refreshing data: {ex.Message}");
            }
        }
    }
}

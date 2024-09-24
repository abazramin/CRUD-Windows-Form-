using BusinessLayer;
using System;
using System.Data;
using System.Windows.Forms;

namespace WindowsFromContact_PresentaionLayer
{
    public partial class AddUpdate : Form
    {

        public enum enMode { AddNew = 0 , Update = 1}
        private enMode _mode;


        int _ContactID;
        clsContacts clsContacts;

        public AddUpdate(int ContactID)
        {
            InitializeComponent();


            _ContactID = ContactID;

            if (_ContactID == -1)
            {
                _mode = enMode.AddNew;
            }
            else
            {
                _mode = enMode.Update;
            }

        }

        private void _FillCountriesCompoBox()
        {
            DataTable data = clsCountries.GetAllCountries();

            foreach (DataRow row in data.Rows)
            {
                comboBox1.Items.Add(row["CountryName"]);
            }

            comboBox1.SelectedIndex = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddUpdate_Load(object sender, EventArgs e)
        {
            _FillCountriesCompoBox();

            if (_mode == enMode.AddNew)
            {
                lbTitle.Text = "Add New Contact";
                clsContacts = new clsContacts();
                return;
            } 

            clsContacts = clsContacts.Find(_ContactID);

            if (clsContacts == null)
            {
                MessageBox.Show("This for will be closed becouse the contact not found", "Error", MessageBoxButtons.OK);
                this.Close();
                return;
            }
            lbTitle.Text = "Update Contact [" + clsContacts.ID.ToString() + "]";
            tbCountactID.Text = clsContacts.ID.ToString();
            tbFirstName.Text = clsContacts.FirstName;
            tbLastNama.Text = clsContacts.LastName;
            tbEmail.Text = clsContacts.Email;
            tbPhone.Text = clsContacts.Phone;
            dateTimePicker1.Text = clsContacts.DateOfBirth.ToString();
            comboBox1.Text = clsContacts.CountryID.ToString();
            tbAddress.Text = clsContacts.Address;


            //if (clsContacts.ImagePath != null)
            //{
            //    picImsge.Load(clsContacts.ImagePath);
            //}
            //else
            //{
            //    return;
            //}

            comboBox1.SelectedIndex = comboBox1.FindString(clsCountries.Find(clsContacts.ID).Countries);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int CountryID = clsCountries.Find(comboBox1.Text).ID;

            clsContacts.FirstName = tbFirstName.Text;
            clsContacts.LastName = tbLastNama.Text;
            clsContacts.Email = tbEmail.Text;
            clsContacts.Phone = tbPhone.Text;
            clsContacts.Address = tbAddress.Text;
            clsContacts.DateOfBirth = dateTimePicker1.Value;
            clsContacts.CountryID = CountryID;


            if (clsContacts.Save())
            {
                MessageBox.Show("Update was successfully", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
            else
            {
                MessageBox.Show("Error an Save Contact", "Wrong", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


            _mode = enMode.Update;
            lbTitle.Text = "Contact ID[" + clsContacts.ID + "]";
        }
    }
}

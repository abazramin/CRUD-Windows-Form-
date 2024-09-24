using BusinessLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFromContact_PresentaionLayer
{
    public partial class MainScreen : Form
    {
        public MainScreen()
        {
            InitializeComponent();
        }

        private void _RefrechContactList()
        {
            dgvAllAcontact.DataSource = clsContacts.GetAllContact();
        }

        private void btnAddNewContact_Click(object sender, EventArgs e)
        {
            AddUpdate addUpdate = new AddUpdate(-1);
            addUpdate.ShowDialog();
        }

        private void MainScreen_Load(object sender, EventArgs e)
        {
            _RefrechContactList();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void eidtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AddUpdate frm = new AddUpdate((int)dgvAllAcontact.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefrechContactList();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Delete [" + dgvAllAcontact.CurrentRow.Cells[0].Value +"] " , "Delete Contact" , MessageBoxButtons.OKCancel , MessageBoxIcon.Stop) == DialogResult.OK)
            {
                if (clsContacts.DeleteContact((int)dgvAllAcontact.CurrentRow.Cells[0].Value))
                {
                    MessageBox.Show("Delete was successfully", "Done", MessageBoxButtons.OK, MessageBoxIcon.Hand);
                    _RefrechContactList();
                }
                else
                {
                    MessageBox.Show("Delete was Feild", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
        }
    }
}

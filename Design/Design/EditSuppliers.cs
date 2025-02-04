﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Design
{
    public partial class update_Supplier : Form
    {
        // Initialize SQL connection
        private int ID;
        private DataGridView dataGridView1;

        //Warehouses_CompanyEntities1 context = new Warehouses_CompanyEntities1();


        public update_Supplier(DataGridView dataGridView1, int id, string name, int phone, string email, string adress)
        {
            InitializeComponent();
            // Assign the dataGridViewProducts control to a field
            this.dataGridView1 = dataGridView1;
            ID = id;

            // Display the data in textboxes
            //idText.Text = id.ToString();
            textName.Text = name;
            textPhone.Text = phone.ToString();
            textEamil.Text = email;
            textAdress.Text = adress;
        }

        public void valdiation()
        {
            //Valdiation ..........
            //if (string.IsNullOrWhiteSpace(idText.Text))
            //{
            //    MessageBox.Show("Please Enter Id");
            //    return;
            //}
            if (string.IsNullOrWhiteSpace(textName.Text))
            {
                MessageBox.Show("Please Enter Name");
                return;
            }
            if (string.IsNullOrWhiteSpace(textEamil.Text))
            {
                MessageBox.Show("Please Enter Email");
                return;
            }
            if (string.IsNullOrWhiteSpace(textPhone.Text))
            {
                MessageBox.Show("Please Enter phone");
                return;
            }
           
            if (string.IsNullOrWhiteSpace(textAdress.Text))
            {
                MessageBox.Show("Please Enter Adress");
                return;
            }

        }
       
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void updateBtn_Click_1(object sender, EventArgs e)
        {
            // Validation
            valdiation();

            // Get the modified data from textboxes
            string newName = textName.Text;
            int newPhone;
            if (!int.TryParse(textPhone.Text, out newPhone))
            {
                throw new ArgumentException("Please enter a valid phone number.");
            }
            string newEmail = textEamil.Text;
            string newAddress = textAdress.Text;

            try
            {

                // Update the corresponding row in the main form's DataGridView
                DataGridViewRow row = dataGridView1.Rows
                    .Cast<DataGridViewRow>()
                    .Where(r => r.Cells["ID"].Value.ToString().Equals(ID.ToString()))
                    .FirstOrDefault();

                if (row != null)
                {
                    row.Cells["Name"].Value = newName;
                    row.Cells["Phone"].Value = newPhone;
                    row.Cells["Email"].Value = newEmail;
                    row.Cells["Address"].Value = newAddress;
                }

                // Save changes to the database
                using (var context = new Warehouses_CompanyEntities1())
                {
                    int SupplierId = int.Parse(row.Cells["ID"].Value.ToString());
                    var Supplier = context.Suppliers.FirstOrDefault(c => c.ID == SupplierId);
                    if (Supplier != null)
                    {

                        Supplier.Name = newName;
                        Supplier.Email = newEmail;
                        Supplier.Address = newAddress;

                        context.SaveChanges();
                    }

                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred while Updating the Supplier: " + ex.Message);
            }

        }

        private void Closebtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void update_Supplier_Load(object sender, EventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lec08_Database_Event_Delagate
{
    public partial class FormMain : Form
    {
        private DBDataContext db = new DBDataContext();
        public FormMain()
        {
            InitializeComponent();


            reloadProducts();
        }

        private void reloadProducts()
        {
            listBoxProducts.Items.Clear();
            listBoxProducts.Items.AddRange(db.Products.ToArray());
        }

        private void listBoxProducts_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (listBoxProducts.SelectedItem != null)
            {
                Product product = listBoxProducts.SelectedItem as Product;
                FormProduct formProd = new FormProduct(product);

                formProd.DataChanged += FormProd_DataChanged;

                formProd.ShowDialog();
            }
        }

        private void FormProd_DataChanged()
        {
            db.SubmitChanges();
            reloadProducts();
        }

        private void buttonAdd_Click(object sender, EventArgs e)
        {
            FormProduct formProd = new FormProduct();

            formProd.DataCreated += FormProd_DataCreated;
            formProd.DataChanged += FormProd_DataChanged;

            formProd.ShowDialog();
        }

        private void FormProd_DataCreated(Product p)
        {
            db.Products.InsertOnSubmit(p);
            db.SubmitChanges();

            reloadProducts();

        }
    }
}

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
    public partial class FormProduct : Form
    {
        public delegate void MyAction();
        public delegate void MyActionProduct(Product p);
        public event MyAction DataChanged;
        public event MyActionProduct DataCreated;

        private Product product;
        public FormProduct()
        {
            InitializeComponent();
        }
        public FormProduct(Product productToEdit)
        {
            InitializeComponent();
            this.product = productToEdit;
            textBoxName.Text = productToEdit.Name;

        }

        private void textBoxName_TextChanged(object sender, EventArgs e)
        {
            //edit - many times after each letter
            if (product != null)
            {
                product.Name = textBoxName.Text;
                if (DataChanged != null)
                {
                    DataChanged();
                }
            }
            //new - only ones after first letter
            else
            {
                if (textBoxName.Text.Length > 0)
                {
                    if (DataCreated != null)
                    {
                        product = new Product();
                        product.Name = textBoxName.Text;
                        DataCreated(product);
                    }
                }
            }
        }
    }
}

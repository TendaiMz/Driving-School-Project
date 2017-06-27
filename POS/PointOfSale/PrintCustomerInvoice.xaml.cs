using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using BUL;

namespace PointOfSale
{
    /// <summary>
    /// Interaction logic for PrintCustomerInvoice.xaml
    /// </summary>
    public partial class PrintCustomerInvoice : UserControl
    {
       public List<SaleDetails> printList = new List<SaleDetails>();
        public PrintCustomerInvoice()
        {
            InitializeComponent();
            //txtDate.Text = DateTime.Today.ToLongDateString();
            //txtName.Text = "Road Test";
            //txtQuntuty.Text = "2hrs";
            LoadFlowDocument();
        }

        void LoadFlowDocument() 
        {

            FlowDocument doc = new FlowDocument();
            Paragraph para = new Paragraph(new Run("LOGO"));
            para.TextAlignment = TextAlignment.Center;

            doc.Blocks.Add(para);

            Table docTable = new Table();
            for (int i = 0; i < 6; i++)
            {
                docTable.Columns.Add(new TableColumn());
            }

            docTable.RowGroups.Add(new TableRowGroup());
            docTable.RowGroups[0].Rows.Add(new TableRow());
            TableRow current = new TableRow();
            current = docTable.RowGroups[0].Rows[0];
            current.Cells.Add(new TableCell(new Paragraph(new Run("Product"))));
            current.Cells.Add(new TableCell(new Paragraph(new Run("Price"))));
            current.Cells.Add(new TableCell(new Paragraph(new Run("Quantity"))));
            current.Cells.Add(new TableCell(new Paragraph(new Run("Amount"))));
            current.Cells.Add(new TableCell(new Paragraph(new Run("Cost"))));

            int count = printList.Count;
            int counter = count - 1;
            foreach (var item in printList)
            {
                docTable.RowGroups[0].Rows.Add(new TableRow());
                current = docTable.RowGroups[0].Rows[count - (counter)];
                //Add the calling code
                current.Cells.Add(new TableCell(new Paragraph(new Run(item.Description.ToString()))));
                //Add the internet TLD
                current.Cells.Add(new TableCell(new Paragraph(new Run(item.Quantity.ToString()))));
                //Add the Time Zone
                current.Cells.Add(new TableCell(new Paragraph(new Run(item.Cost.ToString()))));
                counter = counter - 1;
            }

            doc.Blocks.Add(docTable);
            this.Content = doc;
        }
    }
}

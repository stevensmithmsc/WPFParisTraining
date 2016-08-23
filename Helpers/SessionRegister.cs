using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;

namespace WPFParisTraining.Helpers
{
    public static class SessionRegister
    {
        public static Section Generate()
        {
            Section reg = new Section();
            Table regtable = new Table();
            reg.Blocks.Add(regtable);

            regtable.CellSpacing = 0;            
            regtable.Background = Brushes.White;
            regtable.BorderBrush = Brushes.Black;
            ThicknessConverter tc = new ThicknessConverter();
            regtable.BorderThickness = (Thickness)tc.ConvertFromString("0.05in");

            double[] widths = { 120, 150, 120, 200, 75, 350 };
            int numberOfColumns = 6;
            for (int x = 0; x < numberOfColumns; x++)
            {
                regtable.Columns.Add(new TableColumn());
                regtable.Columns[x].Width = new GridLength(widths[x]);
                //regtable.Columns[x].Background = (x % 2 == 1) ? Brushes.LightGray : Brushes.White;
            }

            TableRowGroup header = new TableRowGroup();
            header.Rows.Add(new TableRow());
            header.Rows[0].FontWeight = System.Windows.FontWeights.Bold;
            header.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Name"))));
            //header.Rows[0].Cells[0].BorderBrush = Brushes.Red;
            //header.Rows[0].Cells[0].BorderThickness = (Thickness)tc.ConvertFromString("0.02in");
            header.Rows[0].Cells[0].Padding = (Thickness)tc.ConvertFromString("0.05in");
            header.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Job Title"))));
            header.Rows[0].Cells[1].Padding = (Thickness)tc.ConvertFromString("0.05in");
            header.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Main Team"))));
            header.Rows[0].Cells[2].Padding = (Thickness)tc.ConvertFromString("0.05in");
            header.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Signature"))));
            header.Rows[0].Cells[3].Padding = (Thickness)tc.ConvertFromString("0.05in");
            header.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Declaration Recieved"))));
            header.Rows[0].Cells[4].FontSize = 10;
            header.Rows[0].Cells[4].Padding = (Thickness)tc.ConvertFromString("0.05in");
            header.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Comments"))));
            header.Rows[0].Cells[5].Padding = (Thickness)tc.ConvertFromString("0.05in");


            regtable.RowGroups.Add(header);

            TableRowGroup data = new TableRowGroup();

            int numberOfPeople = 5;
            for (int x = 0; x < numberOfPeople; x++)
            {
                data.Rows.Add(new TableRow());
                data.Rows[x].Cells.Add(BodyTableCell(new Paragraph(new Run(String.Format("Idiot {0}", x+1)))));
                data.Rows[x].Cells.Add(BodyTableCell(new Paragraph(new Run("Idiot"))));
                data.Rows[x].Cells.Add(BodyTableCell(new Paragraph(new Run("Stupid Team"))));
                data.Rows[x].Cells.Add(BodyTableCell());
                data.Rows[x].Cells.Add(BodyTableCell());
                data.Rows[x].Cells.Add(BodyTableCell(new Paragraph(new Run("An Idiot"))));
                data.Rows[x].Background = (x%2 == 1)?Brushes.Transparent : Brushes.LightGray;
            }

            int numberOfEmptyPlaces = 5;
            for (int x = 0; x < numberOfEmptyPlaces; x++)
            {
                data.Rows.Add(new TableRow());
                data.Rows[x + numberOfPeople].Cells.Add(BodyTableCell());
                data.Rows[x + numberOfPeople].Cells.Add(BodyTableCell());
                data.Rows[x + numberOfPeople].Cells.Add(BodyTableCell());
                data.Rows[x + numberOfPeople].Cells.Add(BodyTableCell());
                data.Rows[x + numberOfPeople].Cells.Add(BodyTableCell());
                data.Rows[x + numberOfPeople].Cells.Add(BodyTableCell());
                data.Rows[x + numberOfPeople].Background = ((x + numberOfPeople) % 2 == 1) ? Brushes.Transparent : Brushes.LightGray;
            }

            regtable.RowGroups.Add(data);


            return reg;
        }

        private static TableCell BodyTableCell(Block myBlock)
        {
            TableCell tc = new TableCell(myBlock);
            ApplyBodyFormating(tc);

            return tc;
        }

        private static TableCell BodyTableCell()
        {
            TableCell tc = new TableCell();
            ApplyBodyFormating(tc);

            return tc;
        }

        private static void ApplyBodyFormating(TableCell tc)
        {
            tc.BorderBrush = Brushes.DarkGray;
            ThicknessConverter conv = new ThicknessConverter();
            tc.BorderThickness = (Thickness)conv.ConvertFromString("0.02in");
            tc.Padding = (Thickness)conv.ConvertFromString("0.03in");
        }
    }
}

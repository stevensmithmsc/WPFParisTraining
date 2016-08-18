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

            regtable.CellSpacing = 10;
            regtable.Background = Brushes.White;
            regtable.BorderBrush = Brushes.Black;
            ThicknessConverter tc = new ThicknessConverter();
            regtable.BorderThickness = (Thickness)tc.ConvertFromString("0.05in");


            int numberOfColumns = 6;
            for (int x = 0; x < numberOfColumns; x++)
            {
                regtable.Columns.Add(new TableColumn());
                regtable.Columns[x].Width = new GridLength(100);
            }

            TableRowGroup header = new TableRowGroup();
            header.Rows.Add(new TableRow());
            header.Rows[0].FontWeight = System.Windows.FontWeights.Bold;
            header.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Name"))));
            header.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Job Title"))));
            header.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Main Team"))));
            header.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Signiture"))));
            header.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Declaration Recieved"))));
            header.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Comments"))));

            regtable.RowGroups.Add(header);

            TableRowGroup data = new TableRowGroup();

            int numberOfPeople = 5;
            for (int x = 0; x < numberOfPeople; x++)
            {
                data.Rows.Add(new TableRow());
                data.Rows[x].Cells.Add(new TableCell(new Paragraph(new Run(String.Format("Idiot {0}", x)))));
                data.Rows[x].Cells.Add(new TableCell(new Paragraph(new Run("Idiot"))));
                data.Rows[x].Cells.Add(new TableCell(new Paragraph(new Run("Stupid Team"))));
                data.Rows[x].Cells.Add(new TableCell());
                data.Rows[x].Cells.Add(new TableCell());
                data.Rows[x].Cells.Add(new TableCell(new Paragraph(new Run("An Idiot"))));
            }

            int numberOfEmptyPlaces = 5;
            for (int x = 0; x < numberOfEmptyPlaces; x++)
            {
                data.Rows.Add(new TableRow());
                data.Rows[x + numberOfPeople].Cells.Add(new TableCell());
                data.Rows[x + numberOfPeople].Cells.Add(new TableCell());
                data.Rows[x + numberOfPeople].Cells.Add(new TableCell());
                data.Rows[x + numberOfPeople].Cells.Add(new TableCell());
                data.Rows[x + numberOfPeople].Cells.Add(new TableCell());
                data.Rows[x + numberOfPeople].Cells.Add(new TableCell());
            }

            regtable.RowGroups.Add(data);


            return reg;
        }
    }
}

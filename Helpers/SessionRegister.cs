using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using WPFParisTraining.Entity;

namespace WPFParisTraining.Helpers
{
    public static class SessionRegister
    {
        public static Section Generate(Sess Session)
        {
            Section reg = new Section();

            Paragraph topBit = new Paragraph();
            
            Image parisLogoImage = new Image();
            parisLogoImage.Source = new BitmapImage(new Uri("C:\\Users\\steven.smith\\Source\\Repos\\WPFParisTraining\\Images\\Paris Logo.png", UriKind.RelativeOrAbsolute));
            //parisLogoImage.Source = new BitmapImage(new Uri("pack://application:,,,/WPFParisTraining;Images/Paris_Logo", UriKind.RelativeOrAbsolute));
            parisLogoImage.Width = 100;
            parisLogoImage.HorizontalAlignment = HorizontalAlignment.Left;
            // var img = new BitmapImage(new Uri("pack://application:,,,/(your project name);component/Resources/PangoIcon.png", UriKind.RelativeOrAbsolute));

            Image pennineLogoImage = new Image();
            pennineLogoImage.Source = new BitmapImage(new Uri("C:\\Users\\steven.smith\\Source\\Repos\\WPFParisTraining\\Images\\trust colour logo.png", UriKind.RelativeOrAbsolute));
            pennineLogoImage.Width = 200;
            pennineLogoImage.HorizontalAlignment = HorizontalAlignment.Right;



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

            TableRowGroup top = new TableRowGroup();
            top.Rows.Add(new TableRow());
            top.Rows[0].Cells.Add(new TableCell(new BlockUIContainer(parisLogoImage)));
            top.Rows[0].Cells[0].RowSpan = 3;
            top.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run("Paris Training Register"))));
            top.Rows[0].Cells[1].ColumnSpan = 4;
            top.Rows[0].Cells[1].FontWeight = FontWeights.Bold;
            top.Rows[0].Cells[1].FontSize = 36;
            top.Rows[0].Cells.Add(new TableCell(new BlockUIContainer(pennineLogoImage)));
            top.Rows.Add(new TableRow());
            top.Rows[1].Cells.Add(new TableCell(new Paragraph(new Run(((DateTime)Session.Strt).ToLongDateString()))));
            top.Rows[1].Cells[0].ColumnSpan = 4;
            top.Rows[1].Cells.Add(new TableCell(new Paragraph(new Run("Start Time: "))));
            top.Rows.Add(new TableRow());
            top.Rows[2].Cells.Add(new TableCell(new Paragraph(new Run(Session.Course.CourseName))));
            top.Rows[2].Cells[0].ColumnSpan = 4;
            top.Rows[2].Cells[0].FontSize = 24;
            top.Rows[2].Cells.Add(new TableCell(new Paragraph(new Run("End Time: "))));
            top.Rows.Add(new TableRow());
            top.Rows[3].Cells.Add(new TableCell(new Paragraph(new Run(String.Format("Trainer: {0}",Session.Trainer.SimpleName)))));
            top.Rows[3].Cells[0].ColumnSpan = 2;
            top.Rows[3].Cells.Add(new TableCell(new Paragraph(new Run(String.Format("Location: {0}", Session.Location.LocationName)))));
            top.Rows[3].Cells[1].ColumnSpan = 4;
            top.Rows.Add(new TableRow());
            top.Rows[4].Cells.Add(new TableCell());

            regtable.RowGroups.Add(top);

            TableRowGroup header = new TableRowGroup();
            header.Rows.Add(new TableRow());
            header.Rows[0].FontWeight = FontWeights.Bold;
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

            int numberOfRows = 0;
            
            foreach (Attendance a in Session.Attendances)
            {
                data.Rows.Add(new TableRow());
                data.Rows[numberOfRows].Cells.Add(BodyTableCell(new Paragraph(new Run(a.Staff.FullName))));
                data.Rows[numberOfRows].Cells.Add(BodyTableCell(new Paragraph(new Run(a.Staff.JobTitle))));
                data.Rows[numberOfRows].Cells.Add(BodyTableCell(new Paragraph(new Run((a.Staff.MainTeam==null)?"":a.Staff.MainTeam.TeamName))));
                data.Rows[numberOfRows].Cells.Add(BodyTableCell(new Paragraph(new Run((a.Outcome!=0)?a.Status.StatusDesc:""))));
                data.Rows[numberOfRows].Cells.Add(BodyTableCell(new Paragraph(new Run((a.Staff.RA==null||a.Staff.RA.Declaration==null)?"":((DateTime)a.Staff.RA.Declaration).ToShortDateString()))));
                data.Rows[numberOfRows].Cells.Add(BodyTableCell(new Paragraph(new Run(a.Comments))));
                data.Rows[numberOfRows].Background = (numberOfRows % 2 == 1)?Brushes.Transparent : Brushes.LightGray;
                numberOfRows++;
            }

            
            for (int x = 0; x < Session.AvailablePlaces; x++)
            {
                data.Rows.Add(new TableRow());
                data.Rows[numberOfRows].Cells.Add(BodyTableCell());
                data.Rows[numberOfRows].Cells.Add(BodyTableCell());
                data.Rows[numberOfRows].Cells.Add(BodyTableCell());
                data.Rows[numberOfRows].Cells.Add(BodyTableCell());
                data.Rows[numberOfRows].Cells.Add(BodyTableCell());
                data.Rows[numberOfRows].Cells.Add(BodyTableCell());
                data.Rows[numberOfRows].Background = (numberOfRows % 2 == 1) ? Brushes.Transparent : Brushes.LightGray;
                numberOfRows++;
            }

            regtable.RowGroups.Add(data);

            TableRowGroup footer = new TableRowGroup();
            footer.Rows.Add(new TableRow());
            footer.Rows[0].FontSize = 10;
            footer.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run(String.Format("Session ID: {0}", Session.ID)))));
            footer.Rows[0].Cells[0].ColumnSpan = 3;
            footer.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run(String.Format("Total Places: {0}", Session.MaxP)))));
            footer.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run(String.Format("Booked: {0}", Session.Bookings)))));
            footer.Rows[0].Cells.Add(new TableCell(new Paragraph(new Run(String.Format("Available: {0}", Session.AvailablePlaces)))));
            footer.Rows.Add(new TableRow());
            footer.Rows[1].Cells.Add(new TableCell());

            regtable.RowGroups.Add(footer);

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

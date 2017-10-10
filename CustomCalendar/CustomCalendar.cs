using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HotelWebSQL.Users
{
    [ToolboxData("<{0}:CustomCalendar runat=server></{0}:CustomCalendar>")]
    public class CustomCalendar : CompositeControl
    {
        Label label;
        TextBox textbox;
        ImageButton imageButton;
        public Calendar calendar;
        [Category("Appearance")]
        public string labelText
        {
            get
            {
                EnsureChildControls();
                return label.Text;
            }
            set
            {
                EnsureChildControls();
                label.Text = value;
            }
        }
        [Category("Appearance")]
        [Description("Sets the image icon for the calendar control")]
        public string ImageButtonImageUrl
        {
            get
            {
                EnsureChildControls();
                return imageButton.ImageUrl != null ? imageButton.ImageUrl : string.Empty;
            }
            set
            {
                EnsureChildControls();
                imageButton.ImageUrl = value;
            }
        }
        [Category("Appearance")]
        [Description("gets or sets the selected date of custom calendar control")]
        public DateTime SelectedDate
        {
            get
            {
                EnsureChildControls();
                return string.IsNullOrEmpty(textbox.Text) ? DateTime.MinValue : Convert.ToDateTime(textbox.Text);
            }
            set
            {
                EnsureChildControls();
                if(value!=null)
                {
                    textbox.Text = value.ToShortDateString();
                    calendar.VisibleDate = value;
                }
                else
                {
                    textbox.Text = "";
                    calendar.VisibleDate = DateTime.Today;
                }
            }
        }
        protected override void CreateChildControls()
        {
            Controls.Clear();
            label = new Label();
            textbox = new TextBox()
            {
                ID = "dateTextBox",
                Width = Unit.Pixel(80)
            };
            imageButton = new ImageButton()
            {
                ID="mycalendarImageButton",
                AlternateText="Kalendarz"
            };
            imageButton.Click += ImageButton_Click;
            calendar = new Calendar()
            {
                ID="CalendarContol"
            };           
            calendar.SelectionChanged += Calendar_SelectionChanged;
            Controls.Add(label);
            Controls.Add(textbox);
            Controls.Add(imageButton);
            calendar.Visible = false;
            Controls.Add(calendar);
            calendar.Height = 0;
        }

        private void Calendar_SelectionChanged(object sender, EventArgs e)
        {
            textbox.Text = calendar.SelectedDate.ToShortDateString();
            calendar.Visible = false;
        }

        private void ImageButton_Click(object sender, ImageClickEventArgs e)
        {
            calendar.Visible = !calendar.Visible;
            if(calendar.Visible)
            {
                calendar.Height = 200;
                if (string.IsNullOrEmpty(textbox.Text))
                {
                    calendar.VisibleDate = DateTime.Today;
                }
                else
                {
                    DateTime output = DateTime.Today;
                    bool isDateTimeConvertionSuccessful = DateTime.TryParse(textbox.Text, out output);
                    calendar.VisibleDate = output;
                    calendar.SelectedDate = output;
                }
            }
            else
            {
                calendar.Height = 0;
            }
        }

        protected override void Render(HtmlTextWriter writer)
        {
            AddAttributesToRender(writer);
            writer.AddAttribute(HtmlTextWriterAttribute.Cellpadding, "1");
            writer.RenderBeginTag(HtmlTextWriterTag.Table);
            writer.RenderBeginTag(HtmlTextWriterTag.Tr);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            label.RenderControl(writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            textbox.RenderControl(writer);
            writer.RenderBeginTag(HtmlTextWriterTag.Td);
            imageButton.RenderControl(writer);
            writer.RenderEndTag();
            writer.RenderEndTag();
            writer.RenderEndTag();
            calendar.RenderControl(writer);
        }
        protected override void RecreateChildControls()
        {
            EnsureChildControls();
        }
    }
}

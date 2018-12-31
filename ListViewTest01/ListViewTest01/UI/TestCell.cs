using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace ListViewTest01.UI
{
    public class TestCell : ViewCell
    {

        public TestCell()
        {
            // Name の Label を用意
            nameLabel.SetBinding(Label.TextProperty, "Id");
            // Age の Label を用意
            var ageLabel = new Label { TextColor = Color.Navy, VerticalOptions = LayoutOptions.Center };
            ageLabel.SetBinding(Label.TextProperty, new Binding("Age", stringFormat: "{0}才"));
            // Image を用意
            var image = new Image { HorizontalOptions = LayoutOptions.EndAndExpand, };
            image.SetBinding(Image.SourceProperty, "Url");

            View = new StackLayout
            {
                Orientation = StackOrientation.Horizontal,
                Padding = 5,
                Children =
                {
                    nameLabel,
                    ageLabel,
                    image,
                },
            };
        }
    }
}

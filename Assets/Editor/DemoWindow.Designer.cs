using uForms;
using UnityEngine;

namespace Demo
{
    partial class DemoWindow
    {
        #region Auto generated code from uForms.
        
        private void InitializeComponent()
        {
            // canvas1
            this.canvas1 = new UFCanvas();
            this.canvas1.Text = "canvas1";
            this.canvas1.Name = "canvas1";
            this.canvas1.IsEnabled = true;
            this.canvas1.IsHidden = false;
            this.canvas1.DrawRect = new Rect(16f, 20f, 250f, 250f);
            // label
            this.label = new UFLabel();
            this.label.Text = "オブジェクト生成";
            this.label.Name = "label";
            this.label.IsEnabled = true;
            this.label.IsHidden = false;
            this.label.DrawRect = new Rect(36f, 35f, 112.5f, 16f);
            this.canvas1.Add(this.label);
            // button
            this.button = new UFButton();
            this.button.Text = "Cube";
            this.button.Name = "button";
            this.button.IsEnabled = true;
            this.button.IsHidden = false;
            this.button.DrawRect = new Rect(32f, 63f, 178.5f, 42.5f);
            this.canvas1.Add(this.button);
            this.Controls.Add(this.canvas1);
            // canvas2
            this.canvas2 = new UFCanvas();
            this.canvas2.Text = "canvas";
            this.canvas2.Name = "canvas2";
            this.canvas2.IsEnabled = true;
            this.canvas2.IsHidden = false;
            this.canvas2.DrawRect = new Rect(280f, 22f, 250f, 250f);
            // objectField
            this.objectField = new UFObjectField();
            this.objectField.Text = "objectField";
            this.objectField.Name = "objectField";
            this.objectField.IsEnabled = true;
            this.objectField.IsHidden = false;
            this.objectField.DrawRect = new Rect(34f, 40f, 184.5f, 16f);
            this.objectField.AllowSceneObject = true;
            this.canvas2.Add(this.objectField);
            // floatSlider
            this.floatSlider = new UFFloatSlider();
            this.floatSlider.Text = "floatSlider";
            this.floatSlider.Name = "floatSlider";
            this.floatSlider.IsEnabled = true;
            this.floatSlider.IsHidden = false;
            this.floatSlider.DrawRect = new Rect(31f, 97f, 177f, 16f);
            this.floatSlider.Value = 1f;
            this.floatSlider.MaxValue = 3f;
            this.floatSlider.MinValue = 0.1f;
            this.canvas2.Add(this.floatSlider);
            // image
            this.image = new UFImage();
            this.image.Text = "image";
            this.image.Name = "image";
            this.image.IsEnabled = true;
            this.image.IsHidden = false;
            this.image.DrawRect = new Rect(27f, 134f, 100f, 100f);
            this.image.GUID = "e6f532133ac63bb49a9b65446503128b";
            this.image.Image = UFUtility.LoadAssetFromGUID<Texture>("e6f532133ac63bb49a9b65446503128b");
            this.canvas2.Add(this.image);
            this.Controls.Add(this.canvas2);
        }
        
        #endregion
        
        private UFCanvas canvas1;
        private UFLabel label;
        private UFButton button;
        private UFCanvas canvas2;
        private UFObjectField objectField;
        private UFFloatSlider floatSlider;
        private UFImage image;
    }
}

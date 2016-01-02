﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ImageControls
{
   
       public partial class ThumbnailBox : UserControl
       {

           private ThumbTextPosition _ThumbTextPosition;
           private bool isSet;
           #region Properties
           [Browsable(true)]
           [Category("Thumbnail")]
           [Description("Set the Selected Color of the Thumbnail")]
        
           public Color SelectedColor { get; set; }
           [Browsable(true)]
           [Category("Thumbnail")]
           [Description("Set the Hover Color of the Thumbnail")]
           public Color HoverColor { get; set; }

           [Browsable(true)]
           [Category("Thumbnail")]
           [Description("Set the Text Position of the Thumanail")]
           public ThumbTextPosition ThumbTextPosition
           {
               get { return _ThumbTextPosition; }
               set
               {
                   _ThumbTextPosition = value;
                   if (_ThumbTextPosition == ImageControls.ThumbTextPosition.Top)
                   {
                       labelPanel.Dock = DockStyle.Top;
                   }
                   else
                   {
                       labelPanel.Dock = DockStyle.Bottom;
                   }
               }
           }

           [Browsable(true)]
           [Category("Thumbnail")]
           [Description("Get or Set the Selected Condition")]
           public bool IsSelected
           {
               get { return isSet; }
               set
               {
                   isSet = value;
                   if (isSet)
                   {
                       thumbLabel.ForeColor = Color.White;
                       this.BackColor = this.SelectedColor;
                   }
                   else
                   {
                       this.BackColor = Color.Silver;
                       thumbLabel.ForeColor = Color.Black;
                   }
               }
           }

           [Browsable(true)]
           [Category("Thumbnail")]
           [Description("Get or Set Caption of the Thumanail")]
           public string Caption
           {
               get { return thumbLabel.Text; }
               set { thumbLabel.Text = value; thumbLabel.Left = (this.Width - thumbLabel.Width) / 2; }
           }

           [Browsable(true)]
           [Category("Thumbnail")]
           [Description("Get or Set Thumnail Image")]
           public Image Thumb
           {
               get { return ThumbPictureBox.BackgroundImage; }
               set
               {
                   ThumbPictureBox.BackgroundImage = value;
               }
           }

           #endregion 
           #region Events

           public delegate void SelectDelegate(ThumbnailBox thumbnailBox);
           [Browsable(true)]
           [Category("Thumbnail")]
           [Description("Event will be raised when Thumbnail Selected")]
           public event SelectDelegate Selected;

           #endregion 


            public ThumbnailBox()
            {
                SelectedColor = Color.DarkBlue;
                HoverColor = Color.Orange;
                _ThumbTextPosition = ThumbTextPosition.Top;
                InitializeComponent();
                ThumbPictureBox.BackgroundImageLayout = ImageLayout.Stretch;
                ThumbPictureBox.Click += delegate
                {
                    if (this.Selected != null)
                    {
                        Selected(this);
                    }
                };
                thumbLabel.Click += delegate
                {
                    if (this.Selected != null)
                    {
                        Selected(this);
                    }
                };
                labelPanel.Click += delegate
                {
                    if (this.Selected != null)
                    {
                        Selected(this);
                    }
                };
                OuterPanel.Click += delegate
                {
                    if (this.Selected != null)
                    {
                        Selected(this);
                    }
                };

                this.Resize += ThumbnailBox_Resize;
                this.Load += ThumbnailBox_Load;
                thumbLabel.Left = (this.Width - thumbLabel.Width) / 2;

            }

            void ThumbnailBox_Load(object sender, EventArgs e)
            {
                adjust();
            }
          
            void ThumbnailBox_Resize(object sender, EventArgs e)
            {
                adjust();
            }
            private void adjust()
            {
                thumbLabel.Left = (this.labelPanel.Width - thumbLabel.Width) / 2;
                OuterPanel.Height = this.Height - 6;
                OuterPanel.Width = this.Width - 6;
                OuterPanel.Left = (this.Width - this.OuterPanel.Width) / 2;
                OuterPanel.Top = (this.Height - OuterPanel.Height) / 2;
            }

            private void ThumbnailBox_MouseEnter(object sender, EventArgs e)
            {
                if (!IsSelected)
                {
                    this.BackColor = this.HoverColor;
                        
                }
            }

            private void ThumbnailBox_MouseLeave(object sender, EventArgs e)
            {
                if (!IsSelected) 
                {
                    this.BackColor = Color.Silver;
                }
            }
           

        }
    
}

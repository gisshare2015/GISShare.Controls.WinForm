using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.WFNew
{
    public class VersionCombineControlItem : BaseItemStackExItem
    {
        // Fields
        private IntegerInputBoxItem m_ibAlpha;
        private IntegerInputBoxItem m_ibBeta;
        private IntegerInputBoxItem m_ibE;
        private IntegerInputBoxItem m_ibRC;
        private LabelItem m_lblAlpha;
        private LabelItem m_lblBeta;
        private LabelItem m_lblE;

        [Browsable(true), Description("选择版本改变后触发（参数中的oldValue无效）"), Category("属性已更改")]
        public event PropertyChangedEventHandler SelectedVersionChanged;

        // Methods
        public VersionCombineControlItem()
        {
            this.m_ibE = new IntegerInputBoxItem
            {
                Size = new Size(50, 20),
                Minimum = 0,
                Maximum = 0x186a0,
                Value = 0
            };
            this.m_lblE = new LabelItem
            {
                Text = "."
            };
            this.m_ibAlpha = new IntegerInputBoxItem
            {
                Size = new Size(50, 20),
                Minimum = 0,
                Maximum = 0x186a0,
                Value = 0
            };
            this.m_lblAlpha = new LabelItem
            {
                Text = "."
            };
            this.m_ibBeta = new IntegerInputBoxItem
            {
                Size = new Size(50, 20),
                Minimum = 0,
                Maximum = 0x186a0,
                Value = 0
            };
            this.m_lblBeta = new LabelItem
            {
                Text = "."
            };
            this.m_ibRC = new IntegerInputBoxItem
            {
                Size = new Size(50, 20),
                Minimum = 0,
                Maximum = 0x186a0,
                Value = 0
            };
            this.BaseItems.Add(this.m_ibE);
            this.BaseItems.Add(this.m_lblE);
            this.BaseItems.Add(this.m_ibAlpha);
            this.BaseItems.Add(this.m_lblAlpha);
            this.BaseItems.Add(this.m_ibBeta);
            this.BaseItems.Add(this.m_lblBeta);
            this.BaseItems.Add(this.m_ibRC);
            ((ILockCollectionHelper)this.BaseItems).SetLocked(true);
            this.ShowBackgroud = false;
            //
            this.m_ibE.ValueChanged += new IntValueChangedHandler(IntValue_ValueChanged);
            this.m_ibAlpha.ValueChanged += new IntValueChangedHandler(IntValue_ValueChanged);
            this.m_ibBeta.ValueChanged += new IntValueChangedHandler(IntValue_ValueChanged);
            this.m_ibRC.ValueChanged += new IntValueChangedHandler(IntValue_ValueChanged);
        }
        void IntValue_ValueChanged(object sender, IntValueChangedEventArgs e)
        {
            string strVersionText = this.VersionText;
            this.OnSelectedVersionChanged(new PropertyChangedEventArgs(typeof(string), strVersionText, this.VersionText));
        }

        // Properties
        [DefaultValue(true), Browsable(true)]
        public bool CanEdit
        {
            get
            {
                return this.m_ibE.CanEdit;
            }
            set
            {
                this.m_ibE.CanEdit = value;
                this.m_ibAlpha.CanEdit = value;
                this.m_ibBeta.CanEdit = value;
                this.m_ibRC.CanEdit = value;
            }
        }

        [Browsable(false)]
        public override int ColumnDistance
        {
            get
            {
                return 0;
            }
            set
            {
                base.ColumnDistance = value;
            }
        }

        [Browsable(false)]
        public override Orientation eOrientation
        {
            get
            {
                return Orientation.Horizontal;
            }
            set
            {
                base.eOrientation = value;
            }
        }

        [Browsable(false)]
        public override bool IsRestrictItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsRestrictItems = value;
            }
        }

        [Browsable(false)]
        public override bool IsStretchItems
        {
            get
            {
                return true;
            }
            set
            {
                base.IsStretchItems = value;
            }
        }

        [Category("属性"), Browsable(true), DefaultValue(true), Description("是否显示月")]
        public bool ShowA
        {
            get
            {
                return this.m_ibAlpha.Visible;
            }
            set
            {
                this.m_ibAlpha.Visible = value;
                this.m_lblAlpha.Visible = value;
            }
        }

        [Category("属性"), Browsable(true), DefaultValue(true), Description("是否显示日")]
        public bool ShowBeta
        {
            get
            {
                return this.m_ibBeta.Visible;
            }
            set
            {
                this.m_ibBeta.Visible = value;
                this.m_lblBeta.Visible = value;
            }
        }

        [Browsable(true), Category("属性"), DefaultValue(true), Description("是否显示年")]
        public bool ShowE
        {
            get
            {
                return this.m_ibE.Visible;
            }
            set
            {
                this.m_ibE.Visible = value;
                this.m_lblE.Visible = value;
            }
        }

        [Category("属性"), DefaultValue(true), Description("是否显示时"), Browsable(true)]
        public bool ShowRC
        {
            get
            {
                return this.m_ibRC.Visible;
            }
            set
            {
                this.m_ibRC.Visible = value;
            }
        }

        [Browsable(true)]
        public string VersionText
        {
            get
            {
                return string.Concat(new object[] { this.m_ibE.Value, ".", this.m_ibAlpha.Value, ".", this.m_ibBeta.Value, ".", this.m_ibRC.Value });
            }
            set
            {
                if (value != null)
                {
                    string[] strArray = value.Split(new char[] { '.' });
                    if (strArray.Length == 4)
                    {
                        int num;
                        if (int.TryParse(strArray[0], out num))
                        {
                            this.m_ibE.Value = num;
                        }
                        if (int.TryParse(strArray[1], out num))
                        {
                            this.m_ibAlpha.Value = num;
                        }
                        if (int.TryParse(strArray[2], out num))
                        {
                            this.m_ibBeta.Value = num;
                        }
                        if (int.TryParse(strArray[3], out num))
                        {
                            this.m_ibRC.Value = num;
                        }
                    }
                }
            }
        }

        //
        protected virtual void OnSelectedVersionChanged(PropertyChangedEventArgs e)
        {
            if (this.SelectedVersionChanged != null) this.SelectedVersionChanged(this, e);
        }
    }


}

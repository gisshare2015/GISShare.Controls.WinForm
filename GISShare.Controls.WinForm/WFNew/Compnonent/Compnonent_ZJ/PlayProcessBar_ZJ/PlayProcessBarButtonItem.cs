using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace GISShare.Controls.WinForm.WFNew
{
    class PlayProcessBarButtonItem : BaseButtonItem, IPlayProcessBarButton, IBaseItemProperty
    {
        public System.Windows.Forms.Orientation eOrientation
        {
            get
            {
                IPlayProcessBarItem pPlayProcessBarItem = this.pOwner as IPlayProcessBarItem;
                if (pPlayProcessBarItem == null) return System.Windows.Forms.Orientation.Horizontal;
                return pPlayProcessBarItem.eOrientation;
            }
        }

        public PlayProcessBarButtonItem(SliderButtonStyle sliderButtonStyle)
        {
            base.Name = "GISShare.Controls.WinForm.WFNew.PlayProcessBarButtonItem";
            base.Text = "���Ž�����[����]";
            base.ShowNomalState = true;
            base.eDisplayStyle = DisplayStyle.eNone;
        }

        #region IBaseItemProperty
        [Browsable(false), Description("��������״̬"), Category("����")]
        BaseItemStyle IBaseItemProperty.eBaseItemStyle
        {
            get { return BaseItemStyle.eComponentBaseItem; }
        }

        [Browsable(false), Description("��ȡ������������Ϊ������������Ϊ������"), Category("����")]
        IBaseItem3 IBaseItemProperty.DependObject
        {
            get { return this.pOwner as IBaseItem3; }
        }
        #endregion

        public override DismissPopupStyle eDismissPopupStyle
        {
            get
            {
                return DismissPopupStyle.eNoDismiss;
            }
        }

        public override bool Enabled
        {
            get
            {
                IPlayProcessBarItem pPlayProcessBarItem = this.pOwner as IPlayProcessBarItem;
                if (pPlayProcessBarItem == null) return base.Enabled;
                return pPlayProcessBarItem.Enabled;
            }
            set
            {
                base.Enabled = value;
            }
        }

        public override BaseItemState eBaseItemState
        {
            get
            {
                return this.Enabled ? base.eBaseItemState : BaseItemState.eDisabled;
            }
        }

        public override System.Drawing.Rectangle DisplayRectangle
        {
            get
            {
                IPlayProcessBarItem pPlayProcessBarItem = this.pOwner as IPlayProcessBarItem;
                if (pPlayProcessBarItem == null) return base.DisplayRectangle;
                return pPlayProcessBarItem.SliderButtonRectangle;
            }
        }

        protected override void OnDraw(System.Windows.Forms.PaintEventArgs pevent)
        {
            WFNew.WFNewRenderer.WFNewRendererStrategy.OnRenderPlayProcessBarButton(new ObjectRenderEventArgs(pevent.Graphics, this, this.DisplayRectangle));//
        }
    }
}


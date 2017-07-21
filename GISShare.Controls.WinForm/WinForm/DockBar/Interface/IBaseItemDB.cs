using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace GISShare.Controls.WinForm.DockBar
{
    public interface IBaseItemDB : WinForm.IImageItem
    {
        //event EventHandler AvailableChanged;
        //event EventHandler BackColorChanged;
        //event EventHandler Click;
        //event EventHandler DisplayStyleChanged;
        //event EventHandler DoubleClick;
        //event DragEventHandler DragDrop;
        //event DragEventHandler DragEnter;
        //event EventHandler DragLeave;
        //event DragEventHandler DragOver;
        //event EventHandler EnabledChanged;
        //event EventHandler ForeColorChanged;
        //event GiveFeedbackEventHandler GiveFeedback;
        //event EventHandler LocationChanged;
        //event MouseEventHandler MouseDown;
        //event EventHandler MouseEnter;
        //event EventHandler MouseHover;
        //event EventHandler MouseLeave;
        //event MouseEventHandler MouseMove;
        //event MouseEventHandler MouseUp;
        //event EventHandler OwnerChanged;
        //event PaintEventHandler Paint;
        //event QueryAccessibilityHelpEventHandler QueryAccessibilityHelp;
        //event QueryContinueDragEventHandler QueryContinueDrag;
        //event EventHandler RightToLeftChanged;
        //event EventHandler TextChanged;
        //event EventHandler VisibleChanged;

        //
        //
        //
      
        //Image BackgroundImage { get; set; }
        //ImageLayout BackgroundImageLayout { get; set; }
        //Rectangle Bounds { get; }
        //bool CanSelect { get; }
        //Rectangle ContentRectangle { get; }
        //ToolStripItemDisplayStyle DisplayStyle { get; set; }
        //DockStyle Dock { get; set; }
        //bool DoubleClickEnabled { get; set; }
        //bool Enabled { get; set; }
        //Font Font { get; set; }
        //Color ForeColor { get; set; }
        //int Height { get; set; }
        //Image Image { get; set; }
        //ContentAlignment ImageAlign { get; set; }
        //int ImageIndex { get; set; }
        //string ImageKey { get; set; }
        //ToolStripItemImageScaling ImageScaling { get; set; }
        //Color ImageTransparentColor { get; set; }
        //bool IsDisposed { get; }
        //bool IsOnDropDown { get; }
        //bool IsOnOverflow { get; }
        //Padding Margin { get; set; }
        //MergeAction MergeAction { get; set; }
        //int MergeIndex { get; set; }
        //string Name { get; set; }
        //ToolStripItemOverflow Overflow { get; set; }
        //ToolStrip Owner { get; set; }
        //ToolStripItem OwnerItem { get; }
        //Padding Padding { get; set; }
        //ToolStripItemPlacement Placement { get; }
        //bool Pressed { get; }
        //RightToLeft RightToLeft { get; set; }
        //bool RightToLeftAutoMirrorImage { get; set; }
        //bool Selected { get; }
        //Size Size { get; set; }
        //object Tag { get; set; }
        //string Text { get; set; }
        //ContentAlignment TextAlign { get; set; }
        //ToolStripTextDirection TextDirection { get; set; }
        //TextImageRelation TextImageRelation { get; set; }
        //string ToolTipText { get; set; }
        //bool Visible { get; set; }
        //int Width { get; set; }
        //
        string Category { get; set; }
        //int RecordID { get; }

        //
        //
        //
        
        //DragDropEffects DoDragDrop(object data, DragDropEffects allowedEffects);        
        //ToolStrip GetCurrentParent();        
        //Size GetPreferredSize(Size constrainingSize);        
        //void Invalidate();        
        //void Invalidate(Rectangle r);
        //void PerformClick(); void ResetBackColor();        
        //void ResetDisplayStyle();        
        //void ResetFont();        
        //void ResetForeColor();        
        //void ResetImage();        
        //void ResetMargin();        
        //void ResetPadding();        
        //void ResetRightToLeft();        
        //void ResetTextDirection();        
        //void Select();         
        //string ToString();
        //
        ToolStripItem Clone();
    }
}

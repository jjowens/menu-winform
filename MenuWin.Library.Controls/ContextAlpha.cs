using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MenuWin.Library.Controls
{
    public partial class ContextAlpha : ContextMenuStrip
    {
        public ContextAlpha()
        {
            InitializeComponent();
            init();
        }

        private enum action_types
        {
            SelectAll,
            SelectNone,
            Invert,
            DoNothing
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
        }

        public void init()
        {
            this.Items.Add(new ToolStripMenuItem("Select All", null, selectAll_ItemClicked));
            this.Items.Add(new ToolStripMenuItem("Select None", null, selectNone_ItemClicked));
            this.Items.Add(new ToolStripMenuItem("Invert Selection", null, invert_ItemClicked));
        }

        private void selectAll_ItemClicked(Object sender, EventArgs e)
        {
            handleSenderCheckboxes(sender);
        }

        private void selectNone_ItemClicked(Object sender, EventArgs e)
        {
            handleSenderCheckboxes(sender);
        }

        private void invert_ItemClicked(Object sender, EventArgs e)
        {
            handleSenderCheckboxes(sender);
        }

        private void handleSenderCheckboxes(Object sender)
        {
            Debug.WriteLine(sender.ToString());

            action_types action = action_types.DoNothing;

            ToolStripItem item = (sender as ToolStripItem);

            switch(item.Text)
            {
                case "Select All":
                    action = action_types.SelectAll;
                    break;
                case "Select None":
                    action = action_types.SelectNone;
                    break;
                case "Invert Selection":
                    action = action_types.Invert;
                    break;
                default:
                    action = action_types.DoNothing;
                    break;
            }

            if (action == action_types.DoNothing)
            {
                return;
            }

            if (item != null)
            {
                ContextMenuStrip owner = item.Owner as ContextMenuStrip;

                if (owner != null)
                {
                    Debug.WriteLine(owner.SourceControl.Text);

                    GroupBox groupBox = owner.SourceControl as GroupBox;

                    if (groupBox != null)
                    {
                        if (groupBox.Controls.Count > 0)
                        {
                            for (int i = 0; i < groupBox.Controls.Count; i++)
                            {
                                CheckBox checkBox = groupBox.Controls[i] as CheckBox;

                                if (checkBox != null)
                                {
                                    Boolean checkboxVal = checkBox.Checked;
                                    switch(action)
                                    {
                                        case action_types.SelectAll:
                                            checkboxVal = true;
                                            break;
                                        case action_types.SelectNone:
                                            checkboxVal = false;
                                            break;
                                        case action_types.Invert:
                                            checkboxVal = !checkboxVal;
                                            break;
                                    }

                                    checkBox.Checked = checkboxVal;
                                }
                            }
                        }
                    }
                }

            }
        }
    }
}

using DevExpress.XtraEditors;
using DevExpress.XtraLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DevControlHandler
{
    public class DevlayoutControlGroupHandler
    {
        // Create an item within a specified group,
        // bound to a specified data field with the specified editor
        private LayoutControlItem CreateItemWithBoundEditor(BaseEdit editor, object dataSource,
           string dataMember, LayoutControlGroup parentGroup)
        {
            editor.DataBindings.Add("EditValue", dataSource, dataMember);
            LayoutControlItem item = parentGroup.AddItem(dataMember, editor) as LayoutControlItem;
            return item;
        }

    }
}

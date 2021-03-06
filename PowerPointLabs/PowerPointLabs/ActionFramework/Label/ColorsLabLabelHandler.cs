﻿using PowerPointLabs.ActionFramework.Common.Attribute;
using PowerPointLabs.ActionFramework.Common.Interface;

namespace PowerPointLabs.ActionFramework.Label
{
    [ExportLabelRibbonId("ColorsLabButton")]
    class ColorsLabLabelHandler : LabelHandler
    {
        protected override string GetLabel(string ribbonId)
        {
            return TextCollection.ColorPickerButtonLabel;
        }
    }
}

using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Areas.HelpPage.ModelDescriptions
{
    public class EnumTypeModelDescription : ModelDescription
    {
        public EnumTypeModelDescription()
        {
            Values = new Collection<EnumValueDescription>();
        }

        public Collection<EnumValueDescription> Values { get; private set; }
    }
}
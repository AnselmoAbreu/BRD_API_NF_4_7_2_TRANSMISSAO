using System;
using System.Reflection;

namespace BRD_API_NF_4_7_2_TRANSMISSAO.Areas.HelpPage.ModelDescriptions
{
    public interface IModelDocumentationProvider
    {
        string GetDocumentation(MemberInfo member);

        string GetDocumentation(Type type);
    }
}
using System.ComponentModel;

namespace Authentication.Core.SharedKernel.Enums
{
    public enum RoleClaimOperationType
    {
        [Description("Read")]
        Read,

        [Description("Create")]
        Create,

        [Description("Update")]
        Update,

        [Description("Delete")]
        Delete
    }
}
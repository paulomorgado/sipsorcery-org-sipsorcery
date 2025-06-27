using System;

[assembly: global::NetEscapades.EnumGenerators.EnumExtensions<global::SIPSorcery.Net.IceRolesEnum>()]

#if NETFRAMEWORK || NETSTANDARD
namespace SIPSorcery.Net
{
    static partial class IceRolesEnumExtensions
    {
        public static bool TryParse(
                ReadOnlySpan<char> name,
                out IceRolesEnum value,
                bool ignoreCase)
            => System.Enum.TryParse<IceRolesEnum>(name.ToString(), ignoreCase, out value);
    }
}
#endif

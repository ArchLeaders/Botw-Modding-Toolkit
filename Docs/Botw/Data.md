# Botw.Data

A class to store paths and data used by Botw Mod Manager.

---

```cs
/// <summary>
/// <para>A class to store paths and data used by Botw Mod Manager.</para>
/// </summary>
public class Data
{
        /// <summary>
        /// <list type="bullet">
        /// <item><description><para><c>%localappdata%\Botw-MM</c></para></description></item>
        /// </list>
        /// </summary>
        public static readonly string root = $"{Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)}\\Botw-MM";
)

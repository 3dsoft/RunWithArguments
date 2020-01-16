using Microsoft.Win32;

namespace RegisterContextMenu
{
    public static class RegisterContextMenu
    {
        static string menuName = "exefile\\shell\\NewMenuOption";
        static string menuCommand = "exefile\\shell\\NewMenuOption\\command";

        static string menuName2 = "Directory\\Background\\shell\\RunWithArgs";
        static string menuCommand2 = "Directory\\Background\\shell\\RunWithArgs\\command";

        public static void Register(string path)
        {
            RegistryKey regmenu = null;
            RegistryKey regcmd = null;

            try
            {
                regmenu = Registry.ClassesRoot.CreateSubKey(menuName);
                regmenu?.SetValue("", "Run With Args");
                regcmd = Registry.ClassesRoot.CreateSubKey(menuCommand);
                regcmd?.SetValue("", "\"" + path + "\"" + "%1");

                regmenu = Registry.ClassesRoot.CreateSubKey(menuName2);
                regmenu?.SetValue("", "Run With Args");
                regcmd = Registry.ClassesRoot.CreateSubKey(menuCommand2);
                regcmd?.SetValue("", "\"" + path + "\"" + " \"%v\"");
            }
            catch { }
            finally
            {
                regmenu?.Close();
                regcmd?.Close();
            }
        }

        public static void Unregister()
        {
            try
            {
                RegistryKey reg = Registry.ClassesRoot.OpenSubKey(menuCommand);
                if (reg != null)
                {
                    reg.Close();
                    Registry.ClassesRoot.DeleteSubKey(menuCommand);
                }

                reg = Registry.ClassesRoot.OpenSubKey(menuName);
                if (reg != null)
                {
                    reg.Close();
                    Registry.ClassesRoot.DeleteSubKey(menuName);
                }

                reg = Registry.ClassesRoot.OpenSubKey(menuCommand2);
                if (reg != null)
                {
                    reg.Close();
                    Registry.ClassesRoot.DeleteSubKey(menuCommand2);
                }
                reg = Registry.ClassesRoot.OpenSubKey(menuName2);
                if (reg != null)
                {
                    reg.Close();
                    Registry.ClassesRoot.DeleteSubKey(menuName2);
                }
            }
            catch { }
        }
    }
}

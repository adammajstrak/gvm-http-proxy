using GvmHttpProxy.Models;
using System.Diagnostics;

namespace GvmHttpProxy.Service
{
    public class GvmService
    {
        public GvmResponse ExecuteGvmCommand(string username, string password, string body)
        {

            string command = "gvm-cli";
            string arguments = $"--gmp-username {username} --gmp-password {password} socket --xml \\\"{body}\\\"";

            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                FileName = "/bin/bash",
                Arguments = $"-c \"{command} {arguments}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = new Process { StartInfo = startInfo })
            {
                process.Start();

                string output = process.StandardOutput.ReadToEnd();
                string error = process.StandardError.ReadToEnd();

                process.WaitForExit();

                return new GvmResponse(output, error);
            }
        }
    }
}

using log4net;
using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Init.SIGePro.Protocollo.SidUmbria.Protocollazione
{
    public class AllegatiService
    {
        ILog _logs = LogManager.GetLogger(typeof(AllegatiService));
        string _host;
        string _username;
        string _password;
        int? _port;

        public AllegatiService(string host, string username, string password, int? port = null)
        {
            this._host = host;
            this._username = username;
            this._password = password;
            this._port = port.HasValue ? port.Value : 22;
        }

        /// <summary>
        /// Al momento non usato, utilizzare solo sftp
        /// </summary>
        /// <param name="fileContents"></param>
        /// <param name="fileName"></param>
        public void UploadFileFtp(byte[] fileContents, string fileName)
        {
            _logs.InfoFormat("UPLOAD DEL FILE {0} IN FTP", fileName);

            // Get the object used to communicate with the server.  
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create($"ftp://{_host}/{fileName}");
            request.Method = WebRequestMethods.Ftp.UploadFile;

            // This example assumes the FTP site uses anonymous logon.  
            request.Credentials = new NetworkCredential(_username, _password);

            // Copy the contents of the file to the request stream.  
            //StreamReader sourceStream = new StreamReader("testfile.txt");
            //byte[] fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
            //sourceStream.Close();
            request.ContentLength = fileContents.Length;

            var requestStream = request.GetRequestStream();
            requestStream.Write(fileContents, 0, fileContents.Length);
            requestStream.Close();

            FtpWebResponse response = (FtpWebResponse)request.GetResponse();

            _logs.InfoFormat("UPLOAD DEL FILE {0} IN FTP AVVENUTO CORRETTAMENTE", fileName);

            response.Close();
        }

        public void UploadSftp(byte[] buffer, string fileName, uint? bufferSize = null)
        {
            _logs.InfoFormat("UPLOAD IN SFTP DEL FILE {0}", fileName);
            _logs.InfoFormat("ISTANZA SftpClient, HOST: {0}, USERNAME: {1}, PASSWORD: {2}, PORT: {3}", _host, _username, _password, 22);
            using (var client = new SftpClient(_host, 22, _username, _password))
            {
                _logs.Info("CONNECT");
                client.Connect();
                _logs.Info("FINE CONNECT");

                _logs.InfoFormat("IS CONNECT: {0}", client.IsConnected);

                if (client.IsConnected)
                {
                    _logs.InfoFormat("BUFFER SIZE HAS VALUE: {0}", bufferSize.HasValue);
                    if (bufferSize.HasValue)
                    {
                        _logs.InfoFormat("SET BUFFER SIZE {0}", bufferSize.Value);
                        client.BufferSize = bufferSize.Value;
                        _logs.InfoFormat("FINE SET BUFFER SIZE {0}", bufferSize.Value);
                    }

                    using (var stream = new MemoryStream(buffer))
                    {
                        _logs.InfoFormat("UPLOAD FILE {0}", fileName);
                        client.UploadFile(stream, fileName, true);
                        _logs.InfoFormat("UPLOAD FILE {0} AVVENUTO CORRETTAMENTE", fileName);
                    }
                }
                else
                {
                    throw new Exception("NON E' POSSIBILE CONNETTERSI AL SERVER SFTP PER L'INVIO DEGLI ALLEGATI");
                }
            }
            _logs.InfoFormat("FINE UPLOAD IN SFTP DEL FILE {0}", fileName);
        }

    }
}

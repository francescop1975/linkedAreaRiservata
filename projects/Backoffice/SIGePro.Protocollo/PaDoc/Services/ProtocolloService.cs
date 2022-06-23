using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Protocollo.Logs;
using Init.SIGePro.Protocollo.Serialize;
using System.Net.Http;
using Init.SIGePro.Protocollo.PaDoc.Protocollazione;
using Init.SIGePro.Protocollo.WsDataClass;
//using System.Xml.Linq;
using Init.Utils;
using System.Xml;
using Init.SIGePro.Manager.Utils;
using System.IO.Compression;
using ICSharpCode.SharpZipLib.Zip;
using System.IO;
using Init.SIGePro.Protocollo.Constants;

namespace Init.SIGePro.Protocollo.PaDoc.Services
{
    public class ProtocolloService
    {
        private static class Constants
        {
            public const string Verb = "verb";
            public const string XmlMetadata = "xmlmetadata";
            public const string RegType = "regtype";
            public const string RegDownload = "reg_download";
            public const string Owner = "owner";
            public const string RegNumber = "regnumber";
            public const string RegYear = "regyear";
            public const string ErrorNode = "/WServer/error";
            public const string ErrorCode = "code";
            //public const string Register = "register";
            public const string SendPec = "send_pec";
        }

        string _url;
        ProtocolloLogs _logs;
        ProtocolloSerializer _serializer;

        public ProtocolloService(string url, ProtocolloLogs logs, ProtocolloSerializer serializer)
        {
            _url = url;
            _logs = logs;
            _serializer = serializer;
        }

        public void Protocolla(string xml, string flusso, string verb)
        {
            using (var client = new HttpClient())
            {
                string xml64 = Base64Utils.Base64Encode(xml);
                var list = new List<KeyValuePair<string, string>>();

                list.Add(new KeyValuePair<string, string>(Constants.XmlMetadata, xml64));
                list.Add(new KeyValuePair<string, string>(Constants.Verb, verb));
                list.Add(new KeyValuePair<string, string>(Constants.RegType, flusso));

                if (flusso == ProtocolloConstants.COD_PARTENZA_DOCAREA)
                    list.Add(new KeyValuePair<string, string>(Constants.SendPec, "1"));


                /*var formContent = new FormUrlEncodedContent(new[] 
                { 
                    new KeyValuePair<string, string>(Constants.XmlMetadata, xml64), 
                    new KeyValuePair<string, string>(Constants.Verb, Constants.Register),
                    new KeyValuePair<string, string>(Constants.RegType, flusso),

                });*/

                var formContent = new FormUrlEncodedContent(list.ToArray());

                _logs.InfoFormat("INVIO RICHIESTA A PROTOCOLLAZIONE, VERB: {0}, REGTYPE: {1}", verb, flusso);
                var response = client.PostAsync(_url, formContent).Result;

                var responseString = StreamUtils.StreamToString(response.Content.ReadAsStreamAsync().Result);

                var xmldoc = new XmlDocument();
                xmldoc.LoadXml(responseString);
                var nodeList = xmldoc.SelectSingleNode(Constants.ErrorNode);

                if (nodeList != null)
                    throw new Exception(String.Format("ERRORE RESTITUITO DAL WEB SERVICE DI PROTOCOLLAZIONE, CODICE: {0}, DESCRIZIONE: {1}", nodeList.Attributes[Constants.ErrorCode].InnerText, nodeList.InnerText));

                _logs.InfoFormat("INVIO RICHIESTA A PROTOCOLLAZIONE, VERB: {0}, REGTYPE: {1} AVVENUTO CON SUCCESSO", verb, flusso);
            }
        }


        public MemoryStream LeggiAllegato(string nomeFile, string annoProtocollo, string numeroProtocollo, string owner)
        {
            using (var client = new HttpClient())
            {
                var url = String.Format("{0}/?{1}={2}&{3}={4}&{5}={6}&{7}={8}", _url, Constants.Verb, Constants.RegDownload, Constants.RegYear, annoProtocollo, Constants.RegNumber, numeroProtocollo, Constants.Owner, owner);

                _logs.InfoFormat("INVIO RICHIESTA A LEGGI PROTOCOLLO (DOWNLOAD PROTOCOLLO), VERB: {0}, NUMERO: {1}, ANNO: {2}, OWNER: {3}, URL: {4}", Constants.RegDownload, numeroProtocollo, annoProtocollo, owner, url);

                client.MaxResponseContentBufferSize = 65536000;
                var response = client.GetAsync(url).Result;

                var ms = new MemoryStream();
                var s = response.Content.ReadAsStreamAsync().Result;

                s.CopyTo(ms);
                ms.Flush();
                //return ms;

                var res = GetFile(ms, nomeFile);
                return res;
            }
        }

        private MemoryStream GetFile(MemoryStream zip, string nomeFile)
        {
            _logs.DebugFormat("Chiamata interna GetFile per estrapolare il file {0} dallo ZIP", nomeFile);
            _logs.DebugFormat("Il contenuto del file ZIP è null? {0}", (zip == null));
            zip.Seek(0, SeekOrigin.Begin);
            var file = new ZipFile(zip);
            _logs.DebugFormat("Il file ZIP {0} è pronto per essere scompattato",file.Name);
            foreach (ZipEntry item in file)
            {
                _logs.Debug($"IS FILE?: {item.IsFile}");
                if (item.IsFile)
                {
                    var ms = new MemoryStream();
                    var zipStream = file.GetInputStream(item);

                    zipStream.CopyTo(ms);
                    ms.Flush();

                    _logs.Debug($"Item.Name: {item.Name}, nomeFile: {nomeFile}");

                    if (item.Name == nomeFile)
                        return ms;
                }
            }

            return null;
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Init.SIGePro.Data;
using System.IO;
using Init.SIGePro.Protocollo.Constants;

namespace Init.SIGePro.Protocollo.Manager
{
    public partial class ProtocolloMgr
    {
        public class NomeFileAllegato
        {
            private readonly string _idComune;
            private readonly string _codicecomune;
            private readonly string _descrizione;
            private readonly string _estensione;
            private readonly string _nomeFile;

            private readonly int _codiceOggetto;

            private readonly int? _vertParamNomeFileMaxLength = (int?)null;

            internal NomeFileAllegato(string idComune, string codicecomune, Oggetti oggetto, string descrizione, string vertParamNomeFileMaxLength)
            {
                if (!String.IsNullOrEmpty(vertParamNomeFileMaxLength))
                    _vertParamNomeFileMaxLength = Convert.ToInt32(vertParamNomeFileMaxLength);

                this._idComune = idComune;
                this._codicecomune = codicecomune;
                this._codiceOggetto = Convert.ToInt32(oggetto.CODICEOGGETTO);
                this._descrizione = Path.GetFileNameWithoutExtension(RimuoviCaratteriNonValidi(descrizione));
                this._nomeFile = RimuoviCaratteriNonValidi(oggetto.NOMEFILE);
                this._estensione = Path.GetExtension(this._nomeFile);

                if (!this._vertParamNomeFileMaxLength.HasValue && this._descrizione.Length > 30)
                    this._descrizione = this._descrizione.Substring(0, 30);

                string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(this._nomeFile);

                if (Path.HasExtension(fileNameWithoutExtension) && Path.GetExtension(this._nomeFile).ToLower() == ProtocolloConstants.P7M)
                    this._estensione = String.Concat(Path.GetExtension(fileNameWithoutExtension), this._estensione);
            }

            private string RimuoviCaratteriNonValidi(string nomeFile)
            {
                return new String(nomeFile.Where(c => !Path.GetInvalidFileNameChars().Contains(c)).ToArray());
            }

            internal string GetEstensione()
            {
                return this._estensione;
            }

            internal string GetNomeCompleto(string vertParamNomeFileOrigine, IEnumerable<ProtocolloAllegati> protoAllegati, bool creaCopiaFile)
            {
                string retVal = String.Concat(this._descrizione, this._estensione);

                switch (vertParamNomeFileOrigine)
                {
                    case "1":
                        retVal = this._nomeFile;
                        break;
                    case "2":
                        retVal = String.Concat(this._idComune, "-", this._codiceOggetto, this._estensione);
                        break;
                    case "3":
                        retVal = String.Concat(this._codicecomune, "-", this._codiceOggetto, "-", this._descrizione, this._estensione);
                        break;
                }

                if (creaCopiaFile)
                    retVal = GetNomeFileCopia(retVal, protoAllegati, 0);

                if (_vertParamNomeFileMaxLength.HasValue && retVal.Length > _vertParamNomeFileMaxLength.Value)
                {
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(retVal);

                    if (Path.HasExtension(fileNameWithoutExtension) && Path.GetExtension(retVal).ToLower() == ProtocolloConstants.P7M)
                        fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileNameWithoutExtension);

                    if (this._estensione.Length >= _vertParamNomeFileMaxLength.Value)
                        throw new Exception(String.Format("SI E' VERIFICATO UN ERRORE DURANTE LA FORMATTAZIONE DEL NOME FILE DELL'ALLEGATO CODICE: {0} NOME: {1}, IL NUMERO DEI CARATTERI INDICATI E' INFERIORE AL CONSENTITO, NUMERO CARATTERI ESTENSIONE {2}: {3}, VALORE PARAMETRO NOMEFILE_MAXLENGTH: {4}", this._codiceOggetto, retVal, this._estensione, this._estensione.Length, _vertParamNomeFileMaxLength.Value));

                    retVal = String.Concat(fileNameWithoutExtension.Substring(0, _vertParamNomeFileMaxLength.Value - this._estensione.Length), this._estensione);
                }

                return retVal;
            }

            public string GetDescrizioneFileCopia(string descrizione, IEnumerable<ProtocolloAllegati> protoAllegati, bool creaCopia, int idx, string vertParamDescrFileMaxLength)
            {
                if (!String.IsNullOrEmpty(vertParamDescrFileMaxLength))
                {
                    int maxlength = Convert.ToInt32(vertParamDescrFileMaxLength); ;

                    if (descrizione.Length > maxlength)
                    {
                        descrizione = descrizione.Substring(0, maxlength);
                    }
                }

                if (!creaCopia)
                    return descrizione;

                string retVal = descrizione;

                var exist = (protoAllegati.Where(x => x.Descrizione == descrizione).Count() > 0);
                if (exist)
                {
                    idx++;
                    retVal = GetDescrizioneFileCopia(String.Format("[{0}] {1}", idx, descrizione), protoAllegati, true, idx, vertParamDescrFileMaxLength);
                }

                return retVal;
            }

            private string GetNomeFileCopia(string nomeFile, IEnumerable<ProtocolloAllegati> protoAllegati, int idx)
            {
                string retVal = nomeFile;

                var exist = (protoAllegati.Where(x => x.NOMEFILE == nomeFile).Count() > 0);
                if (exist)
                {
                    idx++;
                    retVal = GetNomeFileCopia(String.Format("[{0}] {1}", idx, nomeFile), protoAllegati, idx);
                }

                return retVal;
            }
        }
    }
}

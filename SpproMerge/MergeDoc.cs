using InvokeSolutions.Docx;
using InvokeSolutions.Docx.Data;
using Aspose.Words;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpproMerge
{
    public class MergeDoc
    {
        #region Private Members

        internal MergeTemplate MergeTemplate { get; set; }

        #endregion

        #region Constructors

        internal MergeDoc(MergeTemplate mergeTemplate)
        {
            this.MergeTemplate = mergeTemplate;
        }

        #endregion

        #region Public Methods
        
        internal byte[] Merge(List<Byte[]> documentsToAppend = null)
        {
            DocumentDataSource dds = new DocumentDataSource();
            dds.StartingToken = "«";
            dds.EndingToken = "»";
            foreach (var placeHolder in this.MergeTemplate.MergePair)
            {
                dds[placeHolder.PlaceHolder] = placeHolder.Value;
            }

            foreach (var color in this.MergeTemplate.RemoveColor)
            {
                dds[color] = new ColoredDataItem { Action = ColorAction.Remove };
            }
            foreach (var color in this.MergeTemplate.LeaveAndRemoveColor)
            {
                dds[color] = new ColoredDataItem { Action = ColorAction.LeaveAndRemoveColor };
            }
            using (MemoryStream tempStream = new MemoryStream())
            {
                using (var ms = new MemoryStream())
                {
                    MergeTemplate.InputFile.CopyTo(ms);
                    var processedStream = DocumentRenderer.ProcessDocument(ms, dds);
                    if (documentsToAppend != null)
                    {
                        Aspose.Words.Document document = new Document(processedStream);
                        foreach (var docToAppend in documentsToAppend)
                        {
                            Aspose.Words.Document newDoc = new Document(new MemoryStream(docToAppend));
                            document.AppendDocument(newDoc, ImportFormatMode.KeepSourceFormatting);
                        }
                        document.Save(tempStream, SaveFormat.Docx);
                    }
                    else
                    {
                        processedStream.CopyTo(tempStream);
                    }
                }
                
                return tempStream.ToArray();
            }
        }

        #endregion
    }
}

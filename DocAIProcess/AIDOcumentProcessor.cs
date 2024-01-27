using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Azure;
using Azure.AI.DocumentIntelligence;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace DocAIProcess
{
    class AIDOcumentProcessor
    {
        
        public async Task Process()
        {
            string endpoint = "https://docaiinstancetest.cognitiveservices.azure.com/";
            string key = "99f85d54eb344162826ee8005a6644c1";
            AzureKeyCredential credential = new AzureKeyCredential(key);
            DocumentIntelligenceClient client = new DocumentIntelligenceClient(new Uri(endpoint), credential);

            //sample invoice document

            //Uri invoiceUri = new Uri("https://raw.githubusercontent.com/Azure-Samples/cognitive-services-REST-api-samples/master/curl/form-recognizer/sample-invoice.pdf");

            Operation<AnalyzeResult> operation = await client.AnalyzeDocumentAsync(WaitUntil.Completed, "cvresumetestmodel", new AnalyzeDocumentContent 
            {
                Base64Source = BinaryData.FromBytes(File.ReadAllBytes(@"E:\MyStuff\Projects\CVData\Faizan's Resume.pdf"))
            });
            
            AnalyzeResult result = operation.Value; ;
          

            for (int i = 0; i < result.Documents.Count; i++)
            {
                Console.WriteLine($"Document {i}:");
               
                AnalyzedDocument document = result.Documents[i];

                if (document.Fields.TryGetValue("FullName", out DocumentField fullNameFIeld)
                    && fullNameFIeld.Type == DocumentFieldType.String)
                {
                    string fullName = fullNameFIeld.ValueString;
                    Console.WriteLine($"Full Name: '{fullName}', with confidence {fullNameFIeld.Confidence}");
                }

                if (document.Fields.TryGetValue("Email", out DocumentField emailFIeld)
                    && emailFIeld.Type == DocumentFieldType.String)
                {
                    string email = emailFIeld.ValueString;
                    Console.WriteLine($"Email: '{email}', with confidence {emailFIeld.Confidence}");
                }

                if (document.Fields.TryGetValue("PhoneNumber", out DocumentField phonefield)
                    && phonefield.Type == DocumentFieldType.String)
                {
                    string phone = phonefield.ValueString;
                    Console.WriteLine($"Phone: '{phone}', with confidence {phonefield.Confidence}");
                }

                if (document.Fields.TryGetValue("Country", out DocumentField countryNameFIeld)
                    && countryNameFIeld.Type == DocumentFieldType.String)
                {
                    string country = countryNameFIeld.ValueString;
                    Console.WriteLine($"COuntry: '{country}', with confidence {countryNameFIeld.Confidence}");
                }

                if (document.Fields.TryGetValue("City", out DocumentField cityField)
                    && cityField.Type == DocumentFieldType.String)
                {
                    string city = cityField.ValueString;
                    Console.WriteLine($"City: '{city}', with confidence {cityField.Confidence}");
                }

                if (document.Fields.TryGetValue("PinCode", out DocumentField pincodefield)
                    && pincodefield.Type == DocumentFieldType.String)
                {
                    string pincode = pincodefield.ValueString;
                    Console.WriteLine($"pincode: '{pincode}', with confidence {pincodefield.Confidence}");
                }

                //if (document.Fields.TryGetValue("Items", out DocumentField itemsField)
                //    && itemsField.Type == DocumentFieldType.Array)
                //{
                //    foreach (DocumentField itemField in itemsField.ValueArray)
                //    {
                //        Console.WriteLine("Item:");

                //        if (itemField.Type == DocumentFieldType.Object)
                //        {
                //            IReadOnlyDictionary<string, DocumentField> itemFields = itemField.ValueObject;

                //            if (itemFields.TryGetValue("Description", out DocumentField itemDescriptionField)
                //                && itemDescriptionField.Type == DocumentFieldType.String)
                //            {
                //                string itemDescription = itemDescriptionField.ValueString;
                //                Console.WriteLine($"  Description: '{itemDescription}', with confidence {itemDescriptionField.Confidence}");
                //            }

                //            if (itemFields.TryGetValue("Amount", out DocumentField itemAmountField)
                //                && itemAmountField.Type == DocumentFieldType.Currency)
                //            {
                //                CurrencyValue itemAmount = itemAmountField.ValueCurrency;
                //                Console.WriteLine($"  Amount: '{itemAmount.CurrencySymbol}{itemAmount.Amount}', with confidence {itemAmountField.Confidence}");
                //            }
                //        }
                //    }
                //}

                //if (document.Fields.TryGetValue("SubTotal", out DocumentField subTotalField)
                //    && subTotalField.Type == DocumentFieldType.Currency)
                //{
                //    CurrencyValue subTotal = subTotalField.ValueCurrency;
                //    Console.WriteLine($"Sub Total: '{subTotal.CurrencySymbol}{subTotal.Amount}', with confidence {subTotalField.Confidence}");
                //}

                //if (document.Fields.TryGetValue("TotalTax", out DocumentField totalTaxField)
                //    && totalTaxField.Type == DocumentFieldType.Currency)
                //{
                //    CurrencyValue totalTax = totalTaxField.ValueCurrency;
                //    Console.WriteLine($"Total Tax: '{totalTax.CurrencySymbol}{totalTax.Amount}', with confidence {totalTaxField.Confidence}");
                //}

                //if (document.Fields.TryGetValue("InvoiceTotal", out DocumentField invoiceTotalField)
                //    && invoiceTotalField.Type == DocumentFieldType.Currency)
                //{
                //    CurrencyValue invoiceTotal = invoiceTotalField.ValueCurrency;
                //    Console.WriteLine($"Invoice Total: '{invoiceTotal.CurrencySymbol}{invoiceTotal.Amount}', with confidence {invoiceTotalField.Confidence}");
                //}
            }
        }
//set `<your-endpoint>` and `<your-key>` variables with the values from the Azure portal to create your `AzureKeyCredential` and `DocumentIntelligenceClient` instance

    }
}

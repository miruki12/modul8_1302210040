using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Newtonsoft.Json;

namespace modul8_1302210040
{
    class Config
    {
        public String lang;
        public transferDetails transfer;
        public String[] methods;
        public confirmationDetails confirmation;
    }

    class transferDetails
    {
        public int threshold;
        public int low_fee;
        public int high_fee;
    }

    class confirmationDetails
    {
        public String en;
        public String id;
    }
    class BankTransferConfig
    {
        Config defaultConfig = new Config
        {
            lang = "en",
            transfer = new transferDetails
            {
                threshold = 25000000,
                low_fee = 6500,
                high_fee = 15000,
            },
            methods = new String[] { "RTO (real-time)", "SKN", "RGTS", "BI FAST" },
            confirmation = new confirmationDetails
            {
                en = "yes",
                id = "ya",
            }
        };

        public BankTransferConfig()
        {
            writeFileText();
        }

        public void writeFileText()
        {

            File.WriteAllText(@"bank_transfer_config.json", JsonConvert.SerializeObject(defaultConfig));
        }

        public dynamic readFileText()
        {
            return JsonConvert.DeserializeObject(File.ReadAllText(@"bank_transfer_config.json"));
        }

        public void openingMessage()
        {
            if (isEn())
            {
                Console.Write("Please insert the amout of money to transfer: ");


            }
            else
            {
                Console.Write("Masukkan jumlah uang yang akan di-transfer: ");
            }
        }

        public bool isEn()
        {
            dynamic config = readFileText();
            return config.lang == "en";
        }

        public void Transaction(int nominal_transfer)
        {
            dynamic config = readFileText();
            if (nominal_transfer <= Convert.ToInt32(config.transfer.threshold))
            {
                if (isEn())
                {
                    Console.WriteLine("Transfer fee = " + config.transfer.low_fee);
                    Console.WriteLine("Total Amout = " + (config.transfer.low_fee + nominal_transfer));
                }
                else
                {
                    Console.WriteLine("Biaya Transfer = " + config.transfer.low_fee);
                    Console.WriteLine("Total Biaya = " + (config.transfer.low_fee + nominal_transfer));
                }
            }
            else
            {
                if (isEn())
                {
                    Console.WriteLine("Transfer fee = " + config.transfer.high_fee);
                    Console.WriteLine("Total Amout = " + (config.transfer.high_fee + nominal_transfer));
                }
                else
                {
                    Console.WriteLine("Biaya Transfer = " + config.transfer.high_fee);
                    Console.WriteLine("Total Biaya = " + (config.transfer.low_fee + nominal_transfer));
                }
            }
        }

        public void methodTransfer()
        {
            dynamic config = readFileText();
            if (isEn())
            {
                Console.WriteLine("Select transfer method: ");
            }
            else
            {
                Console.WriteLine("Pilih metode transfer: ");
            }
            for (int i = 0; i < 4; i++)
            {
                Console.WriteLine(i + 1 + ". " + config.methods[i]);
            }
        }

        public void confirmation()
        {
            dynamic config = readFileText();
            if (isEn())
            {
                Console.Write("Please type " + config.confirmation.en + " to confirm the transaction ");
            }
            else
            {
                Console.Write("Ketik " + config.confirmation.id + "untuk mengkondirmasi transaksi ");
            }
        }

        public void confirmationReact(String conf)
        {
            if (conf == "yes")
            {
                Console.WriteLine("The transfer is completed");
            }
            else if (conf == "ya")
            {
                Console.WriteLine("Proses transfer berhasil");
            }
            else
            {
                if (isEn())
                {
                    Console.WriteLine("Transfer is cancelled");
                }
                else
                {
                    Console.WriteLine("Transfer dibatalkan");
                }
            }
        }

    }
}
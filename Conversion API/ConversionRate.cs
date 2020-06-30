using System;

namespace CurrencyConversion
{
    public class ConversionRate
    {
        public decimal AED { get; set; }
        public decimal ARS { get; set; }
        public decimal AUD { get; set; }
        public decimal BGN { get; set; }
        public decimal BRL { get; set; }
        public decimal BSD { get; set; }
        public decimal CAD { get; set; }
        public decimal CHF { get; set; }
        public decimal CLP { get; set; }
        public decimal CNY { get; set; }
        public decimal COP { get; set; }
        public decimal CZK { get; set; }
        public decimal DKK { get; set; }
        public decimal DOP { get; set; }
        public decimal EGP { get; set; }
        public decimal EUR { get; set; }
        public decimal FJD { get; set; }
        public decimal GBP { get; set; }
        public decimal GTQ { get; set; }
        public decimal HKD { get; set; }
        public decimal HRK { get; set; }
        public decimal HUF { get; set; }
        public decimal IDR { get; set; }
        public decimal ILS { get; set; }
        public decimal INR { get; set; }
        public decimal ISK { get; set; }
        public decimal JPY { get; set; }
        public decimal KRW { get; set; }
        public decimal KZT { get; set; }
        public decimal MXN { get; set; }
        public decimal MYR { get; set; }
        public decimal NOK { get; set; }
        public decimal NZD { get; set; }
        public decimal PAB { get; set; }
        public decimal PEN { get; set; }
        public decimal PHP { get; set; }
        public decimal PKR { get; set; }
        public decimal PLN { get; set; }
        public decimal PYG { get; set; }
        public decimal RON { get; set; }
        public decimal RUB { get; set; }
        public decimal SAR { get; set; }
        public decimal SEK { get; set; }
        public decimal SGD { get; set; }
        public decimal THB { get; set; }
        public decimal TRY { get; set; }
        public decimal TWD { get; set; }
        public decimal UAH { get; set; }
        public decimal USD { get; set; }
        public decimal UYU { get; set; }
        public decimal ZAR { get; set; }

        public decimal GetRate(CurrenciesEnum currency) =>
            currency switch
            {
                CurrenciesEnum.AED => AED,
                CurrenciesEnum.ARS => ARS,
                CurrenciesEnum.AUD => AUD,
                CurrenciesEnum.BGN => BGN,
                CurrenciesEnum.BRL => BRL,
                CurrenciesEnum.BSD => BSD,
                CurrenciesEnum.CAD => CAD,
                CurrenciesEnum.CHF => CHF,
                CurrenciesEnum.CLP => CLP,
                CurrenciesEnum.CNY => CNY,
                CurrenciesEnum.COP => COP,
                CurrenciesEnum.CZK => CZK,
                CurrenciesEnum.DKK => DKK,
                CurrenciesEnum.DOP => DOP,
                CurrenciesEnum.EGP => EGP,
                CurrenciesEnum.EUR => EUR,
                CurrenciesEnum.FJD => FJD,
                CurrenciesEnum.GBP => GBP,
                CurrenciesEnum.GTQ => GTQ,
                CurrenciesEnum.HKD => HKD,
                CurrenciesEnum.HRK => HRK,
                CurrenciesEnum.HUF => HUF,
                CurrenciesEnum.IDR => IDR,
                CurrenciesEnum.ILS => ILS,
                CurrenciesEnum.INR => INR,
                CurrenciesEnum.ISK => ISK,
                CurrenciesEnum.JPY => JPY,
                CurrenciesEnum.KRW => KRW,
                CurrenciesEnum.KZT => KZT,
                CurrenciesEnum.MXN => MXN,
                CurrenciesEnum.MYR => MYR,
                CurrenciesEnum.NOK => NOK,
                CurrenciesEnum.NZD => NZD,
                CurrenciesEnum.PAB => PAB,
                CurrenciesEnum.PEN => PEN,
                CurrenciesEnum.PHP => PHP,
                CurrenciesEnum.PKR => PKR,
                CurrenciesEnum.PLN => PLN,
                CurrenciesEnum.PYG => PYG,
                CurrenciesEnum.RON => RON,
                CurrenciesEnum.RUB => RUB,
                CurrenciesEnum.SAR => SAR,
                CurrenciesEnum.SEK => SEK,
                CurrenciesEnum.SGD => SGD,
                CurrenciesEnum.THB => THB,
                CurrenciesEnum.TRY => TRY,
                CurrenciesEnum.TWD => TWD,
                CurrenciesEnum.UAH => UAH,
                CurrenciesEnum.USD => USD,
                CurrenciesEnum.UYU => UYU,
                CurrenciesEnum.ZAR => ZAR,
                _ => throw new NotImplementedException(),
            };
    }
}

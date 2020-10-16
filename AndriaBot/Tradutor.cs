using GoogleTranslateFreeApi;
using System;
using System.Net;
using System.Threading.Tasks;

namespace AndriaBot
{
    public static class Tradutor
    {
        private static readonly GoogleTranslator Translator = new GoogleTranslator();
        public static string Traduzir(string text)
        {
            Language portugues = Language.Portuguese; 
            TranslationResult result = Translator.TranslateAsync(text, Language.Auto, portugues).GetAwaiter().GetResult();
            return result.MergedTranslation;
            
        }
        public interface ITranslator
        {
            Task<TranslationResult> TranslateAsync(ITranslatable item);
            Task<TranslationResult> TranslateAsync(string text, Language from, Language to);
            Task<TranslationResult> TranslateLiteAsync(ITranslatable item);
            Task<TranslationResult> TranslateLiteAsync(string text, Language from, Language to);
            IWebProxy Proxy { get; }
            TimeSpan TimeOut { get; }
        }
    }
}

// ✅ ProductSuggestService.cs – GPT ile kategori belirleyip ürünü seçtirir ve aksiyon verisi döner
using ProductProject.Data.Models;
using ProductProject.Repositories;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace ProductProject.Services
{
    public class ProductSuggestService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly CategoryRepository categoryRepository;
        private readonly ProductRepository productRepository;

        public ProductSuggestService(IConfiguration configuration, CategoryRepository categoryRepo, ProductRepository productRepo)
        {
            _apiKey = configuration["OpenAI:ApiKey"];
            categoryRepository = categoryRepo;
            productRepository = productRepo;

            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://api.openai.com/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _apiKey);
        }

        public async Task<string> GetSuggestionAsync(string message)
        {
            var categories = categoryRepository.TList();
            var formattedCategories = string.Join("\n", categories.Select(c => $"- {c.CategoryName}"));

            var categoryRequest = new
            {
                model = "gpt-4-turbo",
                messages = new[]
                {
                    new {
                        role = "system",
                        content = $"""
Kullanıcının yazdığı mesajı analiz et. Aşağıdaki kategorilerden en uygun olanı seç.
Açıklama yapabilirsin ama sadece bu listedeki kategori adlarından birini ver. Yeni kategori oluşturma.
Yanıtın sonunda açıkça şunu yaz: 'İlgili kategori adı: ...'

Mevcut kategoriler:
{formattedCategories}
"""
                    },
                    new {
                        role = "user",
                        content = message
                    }
                }
            };

            var categoryContent = new StringContent(JsonSerializer.Serialize(categoryRequest), Encoding.UTF8, "application/json");
            var categoryResponse = await _httpClient.PostAsync("v1/chat/completions", categoryContent);
            var categoryJson = await categoryResponse.Content.ReadAsStringAsync();

            if (!categoryResponse.IsSuccessStatusCode)
                return JsonSerializer.Serialize(new { error = "API Hatası (kategori): " + categoryResponse.StatusCode });

            using var categoryDoc = JsonDocument.Parse(categoryJson);
            var categoryReply = categoryDoc.RootElement
                                           .GetProperty("choices")[0]
                                           .GetProperty("message")
                                           .GetProperty("content")
                                           .GetString();

            var matchedLine = categoryReply.Split('\n')
                                            .FirstOrDefault(l => l.ToLower().Contains("ilgili kategori"));
            var kategoriAdi = matchedLine?.Split(':').LastOrDefault()?.Trim();

            if (string.IsNullOrEmpty(kategoriAdi))
                return JsonSerializer.Serialize(new { error = "Kategori belirlenemedi." });

            var kategori = categories.FirstOrDefault(c => c.CategoryName == kategoriAdi);
            if (kategori == null)
                return JsonSerializer.Serialize(new { error = "Kategori bulunamadı." });

            var urunler = productRepository.GetByCategoryId(kategori.CategoryID).Take(5).ToList();
            if (urunler.Count == 0)
                return JsonSerializer.Serialize(new { error = $"'{kategoriAdi}' kategorisinde ürün bulunamadı." });

            var urunListesi = string.Join("\n", urunler.Select(u => $"- {u.Name} ({u.Price}₺) - {u.Description}"));

            var productRequest = new
            {
                model = "gpt-4-turbo",
                messages = new[]
                {
                    new {
                        role = "system",
                        content = $"""
Kullanıcının amacı: {message}
Aşağıdaki ürünlerden en uygun olanı seç. Nedenini açıklayıp sonunda şu şekilde yaz:
Önerilen ürün: ...

{urunListesi}
"""
                    }
                }
            };

            var productContent = new StringContent(JsonSerializer.Serialize(productRequest), Encoding.UTF8, "application/json");
            var productResponse = await _httpClient.PostAsync("v1/chat/completions", productContent);
            var productJson = await productResponse.Content.ReadAsStringAsync();

            if (!productResponse.IsSuccessStatusCode)
                return JsonSerializer.Serialize(new { error = "API Hatası (ürün): " + productResponse.StatusCode });

            using var productDoc = JsonDocument.Parse(productJson);
            var finalReply = productDoc.RootElement
                                        .GetProperty("choices")[0]
                                        .GetProperty("message")
                                        .GetProperty("content")
                                        .GetString();

            var onerilenUrun = urunler.FirstOrDefault(u => finalReply.Contains(u.Name));

            return JsonSerializer.Serialize(new
            {
                message = finalReply?.Trim(),
                suggestedProduct = onerilenUrun?.ProductID,
                relatedCategoryId = kategori.CategoryID
            });
        }
    }
}

// ✅ ProductRepository.cs içine eklenecek method örneği
// public List<Product> GetByCategoryId(int categoryId)
// {
//     return _context.Products.Where(p => p.CategoryID == categoryId).ToList();
// }

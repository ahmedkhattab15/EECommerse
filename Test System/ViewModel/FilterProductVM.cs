namespace Test_System.ViewModel
{
    public record FilterProductVM
    {
        public  string? name { get; init; }
        public decimal? minprice { get; init; }
        public decimal? maxprice { get; init; }
        public int? categotyId { get; init; }
        public int? brandId { get; init; }
    }
}

// Record
// نوع بيانات لتخزين البيانات
// القيم تتحدد عند الإنشاء وما تتغيرش بعد كدا : Immutable


//  init يعني اي
//  الخصائص هنا غالبًا يعني تتعين مرة واحدة بس عند الإنشاء وما تتغيرش بعد كده.
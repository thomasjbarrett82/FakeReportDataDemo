using FakeReportDataDemo.Models;

namespace FakeReportDataDemo {
    internal static class Constants {
    }

    internal static class Customers {
        public static Customer BloomMarketing => new Customer { Id = 1, Name = "Bloom Marketing" };
        public static Customer HeartyPancake => new Customer { Id = 2, Name = "Hearty Pancake" };
        public static Customer TheGlowUp => new Customer { Id = 3, Name = "The GlowUp" };
        public static Customer TheLoop => new Customer { Id = 4, Name = "The Loop" };
        public static Customer MyVegetarianDinner => new Customer { Id = 5, Name = "My Vegetarian Dinner" };
        public static Customer GourmetSandwiches => new Customer { Id = 6, Name = "Gourmet Sandwiches" };
        public static Customer OfficeTile => new Customer { Id = 7, Name = "Office Tile" };
        public static Customer HouseBrush => new Customer { Id = 8, Name = "House Brush" };
        public static Customer Raven => new Customer { Id = 9, Name = "Raven" };
        public static Customer UrbanPhilosophy => new Customer { Id = 10, Name = "Urban Philosophy" };
    }

    internal static class Products {
        public static Product Accuprint => new Product { Id = 1, Name = "Accuprint" };
        public static Product BaconSticks => new Product { Id = 2, Name = "Bacon Sticks" };
        public static Product BrigadierCoffee => new Product { Id = 3, Name = "Brigadier Coffee" };
        public static Product CubicleMachine => new Product { Id = 4, Name = "Cubicle Machine" };
        public static Product FeverBlaster => new Product { Id = 5, Name = "Fever Blaster" };
        public static Product GrooveKleen => new Product { Id = 6, Name = "GrooveKleen" };
        public static Product Isoflux => new Product { Id = 7, Name = "Isoflux" };
        public static Product PanSol => new Product { Id = 8, Name = "PanSol" };
        public static Product RedThunder => new Product { Id = 9, Name = "Red Thunder" };
        public static Product StoreYourOats => new Product { Id = 10, Name = "Store Your Oats" };
    }
}

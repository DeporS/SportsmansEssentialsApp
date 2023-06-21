using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

    public class Recipe
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<string> Ingredients { get; set; }
        public List<string> Steps { get; set; }
        public string Icon { get; set; }
        public List<string> Tags { get; set; }

    }

    public partial class RecPage : ContentPage
    {
        private List<Recipe> recipes;

        public RecPage()
        {
            InitializeComponent();

            recipes = new List<Recipe>();

            // TUTAJ PRZEPISY
            var fishRecipe = new Recipe
            {
                Name = "Fish with salt",
                Description = "A simple recipe for fish with salt",
                Ingredients = new List<string>
                {
                    "1 lb. fish fillet",
                    "1 tsp. salt"
                },
                Steps = new List<string>
                {
                    "Preheat oven to 375°F.",
                    "Place the fish fillet in a baking dish.",
                    "Sprinkle salt over the fish.",
                    "Bake for 15-20 minutes, until the fish is cooked through."
                },
                Icon = "salmon.png",
                Tags = new List<string>
                {
                    "fish"
                }
            };
            recipes.Add(fishRecipe);

            var pastaRecipe = new Recipe
            {
                Name = "Spaghetti Carbonara",
                Description = "A classic Italian pasta dish",
                Ingredients = new List<string>
                {
                    "1 lb. spaghetti",
                    "4 oz. pancetta, diced",
                    "2 cloves garlic, minced",
                    "2 large eggs",
                    "1 cup grated Parmesan cheese",
                    "1/2 cup heavy cream",
                    "Salt and pepper, to taste"
                },
                Steps = new List<string>
                {
                    "Cook the spaghetti in a large pot of boiling salted water until al dente.",
                    "While the spaghetti is cooking, heat a large skillet over medium heat. Add the pancetta and cook until crisp and browned, about 5-7 minutes. Add the garlic and cook for another 1-2 minutes, until fragrant.",
                    "In a separate bowl, whisk together the eggs, Parmesan cheese, and heavy cream.",
                    "When the spaghetti is done, reserve 1/2 cup of the cooking water and drain the rest. Add the spaghetti to the skillet with the pancetta and garlic, and toss to combine. Remove the skillet from the heat.",
                    "Add the egg mixture to the spaghetti, and toss quickly to coat the noodles. The heat from the pasta will cook the eggs and create a creamy sauce. If the sauce seems too thick, add some of the reserved cooking water to thin it out.",
                    "Season with salt and pepper to taste, and serve hot."
                },
                Icon = "spaghetti.png",
                Tags = new List<string>
                {
                    "pork"
                }
            };
            recipes.Add(pastaRecipe);

            var veganMushroomPastaRecipe = new Recipe
            {
                Name = "Creamy Mushroom Pasta",
                Description = "A vegan pasta dish with mushrooms and garlic",
                Ingredients = new List<string>
                {
                    "12 oz. pasta",
                    "1 tbsp olive oil",
                    "1 onion, chopped",
                    "3 cloves garlic, minced",
                    "8 oz. mushrooms, sliced",
                    "1/2 cup raw cashews, soaked overnight",
                    "1/2 cup water",
                    "1/4 cup nutritional yeast",
                    "1 tsp salt",
                    "1/2 tsp black pepper"
                },
                Steps = new List<string>
                {
                    "Cook the pasta in a large pot of boiling salted water until al dente.",
                    "While the pasta is cooking, heat the olive oil in a large skillet over medium heat. Add the chopped onion and sauté until soft and translucent, about 5 minutes. Add the minced garlic and sliced mushrooms, and cook until the mushrooms are soft and browned, about 8-10 minutes.",
                    "In a blender, combine the soaked cashews, water, nutritional yeast, salt, and black pepper. Blend until smooth and creamy.",
                    "When the pasta is done, reserve 1/2 cup of the cooking water and drain the rest. Add the pasta to the skillet with the mushroom mixture, and toss to combine. Remove the skillet from the heat.",
                    "Add the cashew cream to the skillet with the pasta and mushrooms, and toss quickly to coat the noodles. If the sauce seems too thick, add some of the reserved cooking water to thin it out.",
                    "Serve hot, garnished with fresh parsley or basil if desired."
                },
                Icon = "mushroom_pasta.png",
                Tags = new List<string>
                {
                    "vegan",
                    "vegetarian"
                }
            };
            recipes.Add(veganMushroomPastaRecipe);

            var stirFryRecipe = new Recipe
            {
                Name = "Chicken Stir-Fry",
                Description = "A quick and easy stir-fry recipe",
                Ingredients = new List<string>
                {
                    "2 boneless, skinless chicken breasts, sliced into strips",
                    "1 red bell pepper, sliced",
                    "1 green bell pepper, sliced",
                    "1 onion, sliced",
                    "1 cup snow peas",
                    "1 cup sliced mushrooms",
                    "2 cloves garlic, minced",
                    "1/4 cup soy sauce",
                    "1/4 cup chicken broth",
                    "1 tablespoon cornstarch",
                    "2 tablespoons vegetable oil",
                    "Salt and pepper, to taste"
                },
                Steps = new List<string>
                {
                    "In a small bowl, whisk together the soy sauce, chicken broth, and cornstarch. Set aside.",
                    "Heat the vegetable oil in a wok or large skillet over high heat. Add the chicken strips and stir-fry for 2-3 minutes, until browned and cooked through.",
                    "Add the bell peppers, onion, snow peas, mushrooms, and garlic to the wok or skillet. Stir-fry for 3-4 minutes, until the vegetables are crisp-tender.",
                    "Pour the soy sauce mixture over the stir-fry and stir to combine. Cook for an additional 1-2 minutes, until the sauce has thickened.",
                    "Season with salt and pepper to taste, and serve hot over rice."
                },
                Icon = "stirfry.png",
                Tags = new List<string>
                {
                    "chicken"
                }
            };
            recipes.Add(stirFryRecipe);



            // Ustaw listę jako źródło danych dla widoku listy
            recipeListView.ItemsSource = recipes;
        }

        // CZARNA MAGIA NIE DOTYKAC
        private async void OnRecipeSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem == null)
                return;

            Recipe selectedRecipe = e.SelectedItem as Recipe;

            string ingredients = "";
            foreach (string ingredient in selectedRecipe.Ingredients)
            {
                ingredients += "- " + ingredient + "\n";
            }

            string steps = "";
            foreach (string step in selectedRecipe.Steps)
            {
                steps += "- " + step + "\n";
            }

            await DisplayAlert(selectedRecipe.Name, "Ingredients:\n" + ingredients + "\nSteps:\n" + steps, "OK");

            recipeListView.SelectedItem = null;
        }

        private void OnTagFilterChanged(object sender, CheckedChangedEventArgs e)
        {
            // Pobierz wszystkie zaznaczone checkboxy
            var checkedCheckboxes = new List<CheckBox> { fish, chicken, pork, beef, vegetarian, vegan }
                .Where(cb => cb.IsChecked)
                .ToList();


            if (!checkedCheckboxes.Any())
            {
                recipeListView.ItemsSource = recipes;
                return;
            }

            // Pobierz wybrane tagi
            var selectedTags = new List<string>();
            if (fish.IsChecked == true)
            {
                selectedTags.Add("fish");
            }
            if(chicken.IsChecked == true) 
            {
                selectedTags.Add("chicken");
            }
            if (pork.IsChecked == true)
            {
                selectedTags.Add("pork");
            }
            if (beef.IsChecked == true)
            {
                selectedTags.Add("beef");
            }
            if (vegetarian.IsChecked == true)
            {
                selectedTags.Add("vegetarian");
            }
            if (vegan.IsChecked == true)
            {
                selectedTags.Add("vegan");
            }



            // Utwórz listę przefiltrowanych przepisów
            var filteredRecipes = recipes.Where(r => r.Tags.Intersect(selectedTags).Any());

            // Zaktualizuj widok listy
            recipeListView.ItemsSource = filteredRecipes;
        }

        

    }
}
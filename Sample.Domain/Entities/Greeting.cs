namespace Sample.Domain.Entities
{
    /// <summary>
    /// A greeting, consisting of a piece of text.
    /// </summary>
    public class Greeting
    {
        /// <summary>
        /// The text of the greeting.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Create a new greeting with the specified text.
        /// </summary>
        public Greeting(string text)
        {
            Text = text;
        }
    }
}

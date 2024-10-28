using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace ChessAPITests
{
    public class CompleteBoardSetupClassData : IEnumerable<object[]>
    {
        private readonly CompleteBoardSetup _completeBoardSetup;

        public CompleteBoardSetupClassData()
        {
            _completeBoardSetup = new CompleteBoardSetup();
        }

        public IEnumerator<object[]> GetEnumerator()
        {
            // Provide the fully initialized board setup as test data
            yield return new object[] { _completeBoardSetup };
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
/*
 Benefits of This Approach

    Reusable Data: You can apply CompleteBoardSetupClassData to any test needing the complete board setup, making it easy to expand testing coverage for capturing, movement validation, and more.
    Customizable Setup: If needed, CompleteBoardSetup can be extended to add specific customizations to the board in future tests without altering the main test logic.

This setup allows you to keep tests focused, concise, and standardized without duplicating the board setup logic across multiple classes. */
using System;
using Xunit;

// ============================================================
//  Tests for LSP.cs — Liskov Substitution Principle
//  Add to: C#/SOLID.Tests/LSPTest.cs
//  Run with: dotnet test (from C#/ folder)
// ============================================================

namespace LSP.Tests
{
    // ── 1. Pigeon tests ───────────────────────────────────────────

    public class PigeonTests
    {
        [Fact]
        public void Pigeon_ImplementsIFlyingBird()
        {
            var pigeon = new Pigeon();
            Assert.IsAssignableFrom<IFlyingBird>(pigeon);
        }

        [Fact]
        public void Pigeon_ImplementsIWalkingBird()
        {
            var pigeon = new Pigeon();
            Assert.IsAssignableFrom<IWalkingBird>(pigeon);
        }

        [Fact]
        public void Pigeon_ImplementsIBird()
        {
            var pigeon = new Pigeon();
            Assert.IsAssignableFrom<IBird>(pigeon);
        }

        [Fact]
        public void Pigeon_Fly_DoesNotThrow()
        {
            var pigeon = new Pigeon();
            var ex = Record.Exception(() => pigeon.Fly());
            Assert.Null(ex);
        }

        [Fact]
        public void Pigeon_Walk_DoesNotThrow()
        {
            var pigeon = new Pigeon();
            var ex = Record.Exception(() => pigeon.Walk());
            Assert.Null(ex);
        }
    }

    // ── 2. Kiwi tests ─────────────────────────────────────────────

    public class KiwiTests
    {
        [Fact]
        public void Kiwi_ImplementsIWalkingBird()
        {
            var kiwi = new Kiwi();
            Assert.IsAssignableFrom<IWalkingBird>(kiwi);
        }

        [Fact]
        public void Kiwi_ImplementsIBird()
        {
            var kiwi = new Kiwi();
            Assert.IsAssignableFrom<IBird>(kiwi);
        }

        [Fact]
        public void Kiwi_DoesNotImplementIFlyingBird()
        {
            // LSP core: Kiwi is excluded from IFlyingBird entirely
            // so it can never violate the contract at runtime
            var kiwi = new Kiwi();
            Assert.False(kiwi is IFlyingBird);
        }

        [Fact]
        public void Kiwi_Walk_DoesNotThrow()
        {
            var kiwi = new Kiwi();
            var ex = Record.Exception(() => kiwi.Walk());
            Assert.Null(ex);
        }
    }

    // ── 3. BirdActions tests ──────────────────────────────────────

    public class BirdActionsTests
    {
        [Fact]
        public void MakeFly_WithPigeon_DoesNotThrow()
        {
            var pigeon = new Pigeon();
            var ex = Record.Exception(() => BirdActions.MakeFly(pigeon));
            Assert.Null(ex);
        }

        [Fact]
        public void MakeWalk_WithPigeon_DoesNotThrow()
        {
            var pigeon = new Pigeon();
            var ex = Record.Exception(() => BirdActions.MakeWalk(pigeon));
            Assert.Null(ex);
        }

        [Fact]
        public void MakeWalk_WithKiwi_DoesNotThrow()
        {
            var kiwi = new Kiwi();
            var ex = Record.Exception(() => BirdActions.MakeWalk(kiwi));
            Assert.Null(ex);
        }
    }

    // ── 4. LSP Compliance tests — the principle itself ────────────

    public class LSPComplianceTests
    {
        [Fact]
        public void Pigeon_CanSubstituteForIFlyingBird()
        {
            // LSP: anywhere IFlyingBird is expected, Pigeon must work
            IFlyingBird bird = new Pigeon();
            var ex = Record.Exception(() => bird.Fly());
            Assert.Null(ex);
        }

        [Fact]
        public void Pigeon_CanSubstituteForIWalkingBird()
        {
            IWalkingBird bird = new Pigeon();
            var ex = Record.Exception(() => bird.Walk());
            Assert.Null(ex);
        }

        [Fact]
        public void Kiwi_CanSubstituteForIWalkingBird()
        {
            // LSP: Kiwi fully satisfies IWalkingBird — no surprises
            IWalkingBird bird = new Kiwi();
            var ex = Record.Exception(() => bird.Walk());
            Assert.Null(ex);
        }

        [Fact]
        public void IFlyingBird_OnlyExposesFly()
        {
            // Interface is correctly segregated
            var methods = typeof(IFlyingBird).GetMethods();
            Assert.Single(methods);
            Assert.Equal("Fly", methods[0].Name);
        }

        [Fact]
        public void IWalkingBird_OnlyExposesWalk()
        {
            var methods = typeof(IWalkingBird).GetMethods();
            Assert.Single(methods);
            Assert.Equal("Walk", methods[0].Name);
        }

        [Fact]
        public void BothBirds_SubstitutableForIWalkingBird_NeitherThrows()
        {
            // Both Pigeon and Kiwi honour the IWalkingBird contract — LSP holds
            IWalkingBird[] walkers = { new Pigeon(), new Kiwi() };
            foreach (var walker in walkers)
            {
                var ex = Record.Exception(() => walker.Walk());
                Assert.Null(ex);
            }
        }
    }
}
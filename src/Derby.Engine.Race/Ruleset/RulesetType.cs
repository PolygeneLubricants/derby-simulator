namespace Derby.Engine.Race.Ruleset;

/// <summary>
///     Specifies the game variation used in the engine.
///     The board game has been published under various versions, each with a slightly different ruleset.
/// </summary>
public enum RulesetType
{
    /// <summary>
    ///     The ruleset for the 1960's edition of Derby, published by Drechsler.
    /// </summary>
    Drechsler,

    /// <summary>
    ///     The ruleset for the 2023 (Second edition of Jägersro) edition of Derby, published by Gameplay Publishing.
    /// </summary>
    Derby2023Traditional
}
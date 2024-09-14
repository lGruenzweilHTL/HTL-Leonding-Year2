namespace Highscore;

public record Player(int Id, string NickName);
public record GameScore(Player Player, int Score, DateTime Date);
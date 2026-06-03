using UnityEngine;

// Boss掉落物,用于加载下一章节
public class BossDrop : CollectableObjectModel
{
    [SerializeField] public ChapterType nextChapter = ChapterType.None;//要加载的下一章节的场景名称
    public override void AfterCollect(PlayerModel collectPlayer)
    {
        base.AfterCollect(collectPlayer);
        //当ChapterType为None时,表明游戏没有下一章节了,所有关卡已经结束了
        if(nextChapter != ChapterType.None)
        {
            ChapterControler.instance?.LoadChapter(nextChapter);    
        }
        else
        {
            SceneLoader.instance?.Load("GameWin",()=>{
                AudioManager.instance.ChangeBgm("InTown");
                AudioManager.instance.PlayBgm();
            });
        }
    }
}

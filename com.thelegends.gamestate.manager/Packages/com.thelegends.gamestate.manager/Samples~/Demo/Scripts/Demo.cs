using UnityEngine;
using TheLegends.Base.GameState;

public class Demo : MonoBehaviour
{
    private void OnEnable()
    {
        // Đăng ký lắng nghe sự kiện khi Component được bật
        GameEvents.OnBoot += HandleBoot;
        GameEvents.OnLoading += HandleLoading;
        GameEvents.OnMainMenu += HandleMainMenu;
        GameEvents.OnPlay += HandlePlay;
        GameEvents.OnPause += HandlePause;
        GameEvents.OnWaitGameOver += HandleWaitGameOver;
        GameEvents.OnRevive += HandleRevive;
        GameEvents.OnGameOver += HandleGameOver;
        GameEvents.OnWaitGameComplete += HandleWaitGameComplete;
        GameEvents.OnGameComplete += HandleGameComplete;
    }

    private void OnDisable()
    {
        // Hủy đăng ký sự kiện khi Component bị tắt hoặc bị xóa để tránh rò rỉ bộ nhớ
        GameEvents.OnBoot -= HandleBoot;
        GameEvents.OnLoading -= HandleLoading;
        GameEvents.OnMainMenu -= HandleMainMenu;
        GameEvents.OnPlay -= HandlePlay;
        GameEvents.OnPause -= HandlePause;
        GameEvents.OnWaitGameOver -= HandleWaitGameOver;
        GameEvents.OnRevive -= HandleRevive;
        GameEvents.OnGameOver -= HandleGameOver;
        GameEvents.OnWaitGameComplete -= HandleWaitGameComplete;
        GameEvents.OnGameComplete -= HandleGameComplete;
    }

    // Các hàm xử lý sự kiện
    private void HandleBoot() => Debug.Log("<color=cyan>[Demo]</color> State Changed: <b>BOOT</b>");
    private void HandleLoading() => Debug.Log("<color=cyan>[Demo]</color> State Changed: <b>LOADING</b>");
    private void HandleMainMenu() => Debug.Log("<color=cyan>[Demo]</color> State Changed: <b>MAIN MENU</b>");
    private void HandlePlay() => Debug.Log("<color=cyan>[Demo]</color> State Changed: <b>PLAY</b>");
    private void HandlePause() => Debug.Log("<color=cyan>[Demo]</color> State Changed: <b>PAUSE</b>");
    private void HandleWaitGameOver() => Debug.Log("<color=cyan>[Demo]</color> State Changed: <b>WAIT GAME OVER</b>");
    private void HandleRevive() => Debug.Log("<color=cyan>[Demo]</color> State Changed: <b>REVIVE</b>");
    private void HandleGameOver() => Debug.Log("<color=cyan>[Demo]</color> State Changed: <b>GAME OVER</b>");
    private void HandleWaitGameComplete() => Debug.Log("<color=cyan>[Demo]</color> State Changed: <b>WAIT GAME COMPLETE</b>");
    private void HandleGameComplete() => Debug.Log("<color=cyan>[Demo]</color> State Changed: <b>GAME COMPLETE</b>");
}

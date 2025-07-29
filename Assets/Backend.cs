using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BackEnd;
using JetBrains.Annotations;

public class BackendManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Init();
        Login();
        ConnectRT();
        SetHandler();
    }   

    public void Init()
    {
        var bro = Backend.Initialize();

        // 뒤끝 초기화에 대한 응답값
        if (bro.IsSuccess())
        {
            Debug.Log("초기화 성공 : " + bro); // 성공일 경우 statusCode 204 Success
        }
        else
        {
            Debug.LogError("초기화 실패 : " + bro); // 실패일 경우 statusCode 400대 에러 발생
        }
    }

    public void Login()
    {
        var bro = Backend.BMember.CustomLogin("test5","test5");

        if(bro.IsSuccess())
        {
            Debug.Log($"로그인 성공");


        }
    }

    public void SetHandler()
    {
        Backend.Notification.OnAuthorize = (bool result, string reason) => {
            Debug.Log("실시간 알림 서버 접속 시도!");

            //접속 이후 처리
            if(result) {
                Debug.Log("실시간 알림 서버 접속 성공!");
            } else {
                Debug.Log("실시간 알림 서버 접속 실패 : 이유 : " + reason);
            }
        };

        Backend.Notification.OnNewNoticeCreated = (string title, string content) => {
            Debug.Log(
                $"[OnNewNoticeCreated(새로운 공지사항 생성)]\n" +
                $"| title : {title}\n" +
                $"| content : {content}\n"
            );
        };
    }

    public void ConnectRT()
    {
        Backend.Notification.Connect();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/************ PlayerAll ***********
tag >> 콜라이더, Layer >> Raycast확인용

◆ Move()
 >> Translate를 FixedUpdate에서 구현하면 벽을 박을때 일부가 타일맵안으로 들어감
    (Rigidbody.velocity(Update) 를 이용해서 구현하면 방지할수 있음)
 >> 벽 붙은 상태에서 점프시 벽에 끼는걸 방지하기 위해 새로운 타일맵에 
    Phsics Material 2d 를 이용해서 마찰력을 없앰
 ▶ 8/22 수정 ◀
 >> 기존에 velocity를 이용하여 움직임을 구현하면 대쉬부분에서 velocity를 인지하지 못하는 상황 발생
    그래서 타일맵안에 들어가는건 따로 플레이어의 TileTouch 오브젝트로 제어하였기에 원래 움직임 형태로 변형
    (타일맵을 통한 마찰력을 플레이어로 옮겼음)

◆ Exceoption()
 >> 예외 사항을 적어둔 함수로 캐릭터가 낙하할때 Idle 모션으로 떨어지는걸 지우기 위해
    점프 또는 더블점프 모션을 조절한다.
 >> 모션을 Jump() 함수에서 사용하면 더블점프할떄 일반 점프 모션이 적용댐

************************************/

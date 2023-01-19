using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
      private float moveSpeed=7f;
      private float jumpSpeed = 8f;
      private CharacterController characterController;
      public Transform cameraPosition;
      public float mouseSens;
      public bool invertX;
      public bool invertY;
      private Vector3 moveVector = Vector3.zero;
      public float gravity = 9.8f;


      void Start(){
            characterController=GetComponent<CharacterController>();
      }
      void Update(){
            //moveVector.x=Input.GetAxis("Horizontal")*moveSpeed * Time.deltaTime;
            //moveVector.z=Input.GetAxis("Vertical")*moveSpeed*Time.deltaTime;

            /*Vector3 moveVr=Input.GetAxis("Vertical")*transform.forward;
            Vector3 moveHr=Input.GetAxis("Horizontal")*transform.right;

            moveVector=moveHr+moveVr;
            moveVector.Normalize();
            moveVector=moveVector * moveSpeed * Time.deltaTime;
            characterController.Move(moveVector);*/
            float moveHr = Input.GetAxis("Horizontal") * moveSpeed;
            float moveVr = Input.GetAxis("Vertical") * moveSpeed;
            moveVector = new Vector3(moveHr, moveVector.y, moveVr);
            if (characterController.isGrounded) {
              if (Input.GetButton("Jump")) {
                moveVector.y = jumpSpeed;
              } else {
                moveVector.y = 0f;
              }
            } 
            ApplyMovement();

            Vector2 mouseVector = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"))*mouseSens;

            if(invertX){
                mouseVector.x=-mouseVector.x;
            }
            if(invertY){
                mouseVector.y=-mouseVector.y;
            }
            transform.rotation=Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + mouseVector.x,transform.rotation.eulerAngles.z);
            cameraPosition.rotation=Quaternion.Euler(cameraPosition.rotation.eulerAngles+new Vector3(mouseVector.y, 0f,0f));

      }
          private void ApplyMovement() {
            moveVector = transform.TransformDirection(moveVector);
            moveVector.y -= this.gravity * Time.deltaTime;
            characterController.Move(moveVector * Time.deltaTime);
          }

}

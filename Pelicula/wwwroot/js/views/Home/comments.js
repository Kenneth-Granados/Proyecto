// $(function(){
//     $(document).on('click',"#add-comment", function(event){
//         $.ajax({
//             type: 'POST',
//             url: "~/Home/AddComment",
//             dataType: "json",
//             cache:false,
//             data: {
//                 IdPelicuta: $("IdPeliculaTextBox").val(),
//                 Comentario1: $("#CommentTextArea").val()
//             },
//             success: function(data){
//                 if(data==1){
//                     $("#comments-container").apped("<p>" + $("#CommentTextArea").val() + "</p>")
//                 }
//             },
//             error: function(XMLHttpRequest, textStatus, errorThrow) {
//                 console.log("Error " + textStatus);     
//             }                       
//         });
//     })
// })
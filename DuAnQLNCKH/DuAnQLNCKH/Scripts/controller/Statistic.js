

$(document).ready(function () {

    

    function Contains(text_one, text_two) {
        if (text_one.indexOf(text_two) != -1)
            return true;
    }

// statis lecture

    $("#Search").keyup(function () {
        var searchText = $("#Search").val().toLowerCase();
       
        $(".Search").each(function () {
            var id = $(this).find(".nametd");
           
            if (Contains($(id).text().toLowerCase(), searchText)) {
                $(this).show();
            }
            else {
                $(this).hide();
            }
        })
    })
 
    $("#StatisticYear").change(function () {
        var searchText = $("#StatisticYear").val();

        $(".Search").each(function () {
            var id = $(this).find(".Year").text().substr(6, 4);

            if (!Contains(id, searchText)) {
                $(this).hide();
            }
            else {
                $(this).show();
            }
        })
    })

    // Tìm kiếm giảng viên

    $("#buttonSearch").click(function () {


         
        //var a = $("#IdP option:selected").val();
        
        //if ($("#IdTy").text() != "--Tất cả loại đề tài--") {
        //    var a = $("#IdTy option:selected").val();
        //    $("#IdTya").val(a);
        //}
        //if ($("#IdF").text()!="--Tất cả khoa--") {
        //    var b = $("#IdF option:selected").val();
        //    $("#IdFa").val(b);
        //}
        //if ($("#IdLe").text() != "--Tất cả giảng viên--") {
        //    var c = $("#IdLe option:selected").val();
        //    $("#IdLea").val(c);
        //}
        var dateSt = $("#DateSt").val();
        var dateEnd = $("#DateEnd").val();

        var dateEnd1 = new Date(dateEnd);
        var dateSt1 = new Date(dateSt);
        $("#DateSt1").val(dateSt);
        $("#DateEnd1").val(dateEnd);
      
        var IdTySearch = $("#IdTy").val();
        var IdFSearch = $("#IdF").val();
        var IdLeSearch = $("#IdLe").val();
        var Pro = $("#Progress").val();
        var list = [];
        $(".Search").each(function () {

            var id = $(this).find(".Year1").val();
            var IdTy = $(this).find(".IdTy").val();
            var IdF = $(this).find(".IdFacu1").val();
            var IdLe = $(this).find(".IdLe1").val();
            var Progress = $(this).find(".Progress1").val();
            //var pointBody = $(this).find(".pointBody");
            if (IdTySearch == "") {
                if (IdFSearch == "") {
                    if (IdLeSearch == "") {
                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id)) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                            
                    }
                    else {

                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                      
                    }
                    
                }
                else {
                    if (IdLeSearch == "") {
                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdF == IdFSearch) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdF == IdFSearch && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        
                    }
                    else {
                         
                        if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch && IdF == IdFSearch) {

                            $(this).show();
                        }
                        else {
                            $(this).hide();
                        }
                    }
                }
            }
            else {

                if (IdFSearch == "") {
                    if (IdLeSearch == "") {
                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdTy == IdTySearch) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdTy == IdTySearch && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                       
                    }
                    else {
                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch && IdTy == IdTySearch) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch && IdTy == IdTySearch && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                    }

                }
                else {
                    if (IdLeSearch == "") {
                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdF == IdFSearch && IdTy == IdTySearch) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdF == IdFSearch && IdTy == IdTySearch && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                    }
                    else {
                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch && IdF == IdFSearch && IdTy == IdTySearch) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch && IdF == IdFSearch && IdTy == IdTySearch && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                    }
                }
            }
            if ($(this).css('display') != 'none') {
                 
                var objCells = this.cells;
                var itemList = {};
                itemList['NameTopic'] = objCells.item(0).innerHTML;
                itemList['NameLec'] = objCells.item(1).innerText;
                 itemList['Type'] = objCells.item(2).innerHTML;
                itemList['DateSt'] = objCells.item(3).innerHTML;
                 itemList['DateEnd'] = objCells.item(4).innerHTML;
                itemList['Expense'] = objCells.item(5).innerHTML;
                itemList['Status'] = objCells.item(6).innerHTML;
                itemList['Point'] = objCells.item(7).innerHTML;

                list.push(itemList);

                   
            }
        })
        $.ajax({
            type: "POST",
            url: "/Statistic/ExportExcel1/",
            data: { list },

            dataType: "json",
            success: function (data) {

            }

        });

    })

   // Tìm kiếm sinh viên

    $("#buttonSearchStu").click(function () {

         
        var dateSt = $("#DateStStu").val();
        var dateEnd = $("#DateEndStu").val();

        var dateEnd1 = new Date(dateEnd);
        var dateSt1 = new Date(dateSt);
         

        var IdTySearch = $("#IdTyStu").val();
        var IdFSearch = $("#IdFStu").val();
        var IdLeSearch = $("#IdSV").val();
        var Pro = $("#ProgressStu").val();
        var list1 = [];
        $(".SearchStu").each(function () {

            var id = $(this).find(".Year1Stu").val();
            var IdTy = $(this).find(".IdTyStu").val();
            var IdF = $(this).find(".IdFacu1Stu").val();
            var IdLe = $(this).find(".IdSV1").val();
            var Progress = $(this).find(".ProgressStu").val();
            if (IdTySearch == "") {
                if (IdFSearch == "") {
                    if (IdLeSearch == "") {
                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id)) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }

                    }
                    else {

                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }

                    }

                }
                else {
                    if (IdLeSearch == "") {
                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdF == IdFSearch) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdF == IdFSearch && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }

                    }
                    else {

                        if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch && IdF == IdFSearch) {

                            $(this).show();
                        }
                        else {
                            $(this).hide();
                        }
                    }
                }
            }
            else {

                if (IdFSearch == "") {
                    if (IdLeSearch == "") {
                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdTy == IdTySearch) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdTy == IdTySearch && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }

                    }
                    else {
                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch && IdTy == IdTySearch) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch && IdTy == IdTySearch && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                    }

                }
                else {
                    if (IdLeSearch == "") {
                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdF == IdFSearch && IdTy == IdTySearch) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdF == IdFSearch && IdTy == IdTySearch && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                    }
                    else {
                        if (Pro == "") {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch && IdF == IdFSearch && IdTy == IdTySearch) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                        else {
                            if (dateSt1 < new Date(id) && dateEnd1 > new Date(id) && IdLe == IdLeSearch && IdF == IdFSearch && IdTy == IdTySearch && Pro == Progress) {

                                $(this).show();
                            }
                            else {
                                $(this).hide();
                            }
                        }
                    }
                }
            }
            if ($(this).css('display') != 'none') {
                 
                    var objCells = this.cells;
                    var itemList = {};
                    itemList['NameTopic'] = objCells.item(0).innerHTML;
                    itemList['NameStu'] = objCells.item(1).innerHTML;
                    itemList['Faculty'] = objCells.item(2).innerHTML;
                    itemList['Type'] = objCells.item(3).innerHTML;
                    itemList['Lec'] = objCells.item(4).innerHTML;
                    itemList['DateSt'] = objCells.item(5).innerHTML;
                    itemList['Time'] = objCells.item(6).innerHTML;
                    itemList['DateEnd'] = objCells.item(7).innerHTML;
                    itemList['Expense'] = objCells.item(8).innerHTML;
                    itemList['Status'] = objCells.item(9).innerHTML;
                    itemList['Point'] = objCells.item(10).innerHTML;

                    list1.push(itemList);
                  
            }
             
        })
        $.ajax({
            type: "POST",
            url: "/Statistic/ExportExcel2/",
            data: { list1 },

            dataType: "json",
            success: function (data) {

            }

        });

    })

})
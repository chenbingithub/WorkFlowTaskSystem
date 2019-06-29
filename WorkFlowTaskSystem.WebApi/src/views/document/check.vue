
<template>
    <div>
      
        <div class="margin-top-10">
            
           <div class="padding-left-10">

                    <Card>
                        <div>
                            
                            
                            <ButtonGroup size="large"  >
                            <Button type="warning"   @click="deletedata" >清除数据</Button>
                            <Button type="primary"  :disabled="disabledbtn" @click="refresh">重新获取</Button>
                             </ButtonGroup>
                            <p>流水号：{{randomnumber}}</p>
                        </div>
                           
                        <p slot="title">
                            <Icon type="ios-analytics"></Icon>
                            校验敷设表
                        </p>
                        <div class="height-492px">
                           <file-upload  :data="uploadList1" @on-uploadlist-change="onResultChange1" :title="title1" :format="['xlsx','jpg','jpeg','png']" ></file-upload>

                            <file-upload :data="uploadList2" @on-uploadlist-change="onResultChange2" :multiple="true" :title="title2" :size="1000" methods="Cable" :randomnumber="randomnumber" :format="['xlsx','xls']" ></file-upload>
                            <br/>
                            
                            <Button type="primary" :loading="loading" long  @click="submit">
                                <span v-if="!loading">检验</span>
                                <span v-else>Loading...</span>
                            </Button>
                    
                        </div>
                        
                    </Card>
                </div>
        </div>
    </div>
</template>

<script>
import fileUpload from './components/uploadlist.vue';
export default {
    name: 'file-uploads',
    components: {
            fileUpload
        },
    data () {
        return {
            title1:"现场实际敷设表",
            uploadList1: [],
            title2:"设计敷设表",
            uploadList2: [],
            randomnumber:abp.randomNumber(),
            disabledbtn:false,
            loading: false
        };
    },
    methods: {
        refresh(){
            this.randomnumber=abp.randomNumber();
        },
        deletedata(){
            location.reload();
        },
       onResultChange1(val){
            this.uploadList1=val;
        },
        onResultChange2(val){
            this.uploadList2=val;
        },
        submit(){
            var number=this.randomnumber;
            var pc=[];    
            this.uploadList2.forEach(function(value,index,arr){
                 pc.push(value.name);
            });
            var $this=this; 
            if(this.uploadList1.length==1&&this.uploadList2.length>0){
                this.loading=true;
                this.$store.dispatch({
                    type:'filemanager/check',
                    data:{
                        RealityTable:this.uploadList1[0].name,
                        DesignTables:pc,
                        NumberNo:number
                    }
                }).then(function (response) {
                    abp.downloadfile(response.data,number+'.xlsx');
                    $this.loading=false;
                }).catch(function (error) {
                    console.log(error);
                    $this.loading=false;
                    
                });
            }else{
                $this.$Message.warning("请先上传文件")
            } 
        }

    },
   watch:{
       uploadList1(){
            let flag1=this.uploadList1.length>0;
            let flag2=this.uploadList2.length>0;
            if(flag1||flag2){
                this.disabledbtn=true;
            }else{
                this.disabledbtn=false;
            }
       },
       uploadList2(){
            let flag1=this.uploadList1.length>0;
            let flag2=this.uploadList2.length>0;
            if(flag1||flag2){
                this.disabledbtn=true;
            }else{
                this.disabledbtn=false;
            }
       }
        
    },
};
</script>

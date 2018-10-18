
<template>
    <div>
      
        <div class="margin-top-10">
            
           <div class="padding-left-10">
                    <Card>
                        <p slot="title">
                            <Icon type="ios-analytics"></Icon>
                            电缆常量表
                        </p>
                        <div class="height-492px">
                           <file-upload  :data="uploadList1" @on-uploadlist-change="onResultChange1" :title="title1" :format="['xlsx','xls','png']" ></file-upload>
                            
                            <br/>
                            <Button type="primary" :loading="loading" long @click="submit">
                                <span v-if="!loading">导入</span>
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
            title1:"电缆常量表",
            uploadList1: [],
            loading: false
        };
    },
    methods: {
       onResultChange1(val){
            this.uploadList1=val;
        },
        submit(){
            var number=abp.randomNumber();
            
            var $this=this; 
            if(this.uploadList1.length==1){
                this.loading=true;
                this.$store.dispatch({
                    type:'cableconstant/Insert',
                    data:{
                        Path:this.uploadList1[0].name,
                        NumberNo:number
                    }
                }).then(function (response) {
                    $this.$Modal.success({
                        title: "提示",
                        content: "导入成功"
                    });
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
};
</script>

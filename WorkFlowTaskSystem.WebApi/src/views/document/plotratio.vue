<template>
    <div>
        <Card>
            <p slot="title">{{'数据管理'|l}}</p>
            <Dropdown slot="extra"  @on-click="handleClickActionsDropdown">
                <a href="javascript:void(0)">
                    {{'操作'|l}}
                    <Icon type="android-more-vertical"></Icon>
                </a>
                <DropdownMenu slot="list">
                        
                    <DropdownItem name='Refresh'>{{'刷新' | l}}</DropdownItem>
                    <DropdownItem name='Create' v-if="persBtn.create" >{{'添加' | l}}</DropdownItem>
                    <DropdownItem name='DeleteAll' v-if="persBtn.create" >{{'删除所有' | l}}</DropdownItem>
                </DropdownMenu>
            </Dropdown>
            
            <Table  :columns="columns"  border :data="plotratios"></Table>
            <Page :total="totalCount" class="margin-top-10" @on-change="pageChange" @on-page-size-change="pagesizeChange" :page-size="pageSize" :current="currentPage" show-sizer show-elevator show-total ></Page>
        </Card>
        <Modal v-model="showModal" :title="L('添加')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                    <Form ref="newplotratioForm" :label-width="100" :rules="newplotratioRule" :model="editplotratio">
                            
                             <FormItem :label="L('通道类型')" prop="passageType" >
                                <Input v-model="editplotratio.passageType"  ></Input>
                            </FormItem>
                            <FormItem :label="L('容积率')" prop="plotRatioLimit" >
                                <Input v-model="editplotratio.plotRatioLimit" number ></Input>
                            </FormItem>  
                           
                                                          
                </Form>                        
            </div>
            <div slot="footer">
                <Button @click="cancel">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
        <Modal v-model="showEditModal" :title="L('编辑')" @on-ok="save" :okText="L('save')" :cancelText="L('cancel')">
            <div>
                <Form ref="plotratioForm" :label-width="100" :rules="plotratioRule" :model="editplotratio">
                            <FormItem :label="L('通道类型')" prop="passageType" >
                                <Input v-model="editplotratio.passageType"  ></Input>
                            </FormItem>
                            <FormItem :label="L('容积率')" prop="plotRatioLimit" >
                                <Input v-model="editplotratio.plotRatioLimit" number ></Input>
                            </FormItem>  
                            
                                           
                </Form>
            </div>
            <div slot="footer">
                <Button @click="cancel">{{'取消'|l}}</Button>
                <Button @click="save" type="primary">{{'确定'|l}}</Button>
            </div>
        </Modal>
    </div>
</template>
<script>
export default {
    methods:{
        create(){
            this.editplotratio={};  
            this.editplotratio.description=abp.randomNumber();
            this.showModal=true;
        },
        cancel(){
            if(!!this.editplotratio.id){
                this.showEditModal=false;
                this.getpage();
            }else{
                this.showModal=false;
                this.getpage();
            }
        },
        async save(){
            if(!!this.editplotratio.id){
                this.$refs.plotratioForm.validate(async (val)=>{
                    if(val){
                        await this.$store.dispatch({
                            type:'plotratio/update',
                            data:this.editplotratio
                        })
                        this.showEditModal=false;
                        this.$Message.success('保存成功！');
                        await this.getpage();
                    }
                })
                
            }else{
                this.$refs.newplotratioForm.validate(async (val)=>{
                    if(val){
                        await this.$store.dispatch({
                            type:'plotratio/create',
                            data:this.editplotratio
                        })
                        this.showModal=false;
                        this.$Message.success('添加成功！');
                        await this.getpage();
                    }
                })
            }
            
        },
        pageChange(page){
            this.$store.commit('plotratio/setCurrentPage',page);
            this.getpage();
        },
        pagesizeChange(pagesize){
            this.$store.commit('plotratio/setPageSize',pagesize);
            this.getpage();
        },
        async getpage(){
            await this.$store.dispatch({
                type:'plotratio/getAll'
            })
        },
        seachKeyChange(){
            this.$store.commit('plotratio/setCurrentPage',1);
            this.getpage();
        },
        onselectionchange(row){
            this.selectedData=row;
        },
        handleClickActionsDropdown(name){
            if(name==='Create'){
                this.create();
            }else if(name==='Refresh'){
                this.getpage();
                this.$Message.success('刷新成功！');

            }else if(name=='DeleteAll'){
                var $this=this;
                this.$store.dispatch({
                    type:'plotratio/deleteAll'
                }).then(function (response) {
                    $this.getpage();
                    $this.$Message.success('刷新成功！');
                }).catch(function (error) {
                    console.log(error);
                    
                });
                
            }
        }
    },
    data(){
        
        return{
            editplotratio:{},
             persBtn:{
                create:true,//abp.auth.isGranted('Pages.plotratios.Create'),
                update:true,//abp.auth.isGranted('Pages.plotratios.Update'),
                delete:true,//abp.auth.isGranted('Pages.plotratios.Delete'),
            },
            showModal:false,
            showEditModal:false,
            selectedData:[],
            seachKey:"",
            newplotratioRule:{
                passageType:[{required:true,message:'passageType is required',trigger: 'blur'}],
                plotRatioLimit:[{validator:abp.validateNumber,trigger: 'blur'}],
                
            },            
            plotratioRule:{
               passageType:[{required:true,message:'passageType is required',trigger: 'blur'}],
                plotRatioLimit:[{validator:abp.validateNumber,trigger: 'blur'}],
            },
            columns:[
            
                    {
                    title:this.L('序号'),
                        type: 'index',
                        width: 61,
                        align: 'center'
                    }
            ,{
                title:this.L('通道类型'),
                 width: 160,
                key:'passageType'
            },{
                title:this.L('容积率'),
                key:'plotRatioLimit'
            },{
                title: this.L('操作'),
                key: 'action',
                width:150,
                render:(h,params)=>{
                    var btns=[];
                            if(this.persBtn.update){
                                var d=h('Button',{
                                            props:{
                                                type:'primary',
                                                size:'small'
                                            },
                                            style:{
                                                marginRight:'5px'
                                            },
                                            on:{
                                                click:()=>{
                                                    this.editplotratio=this.plotratios[params.index];
                                                    this.showEditModal=true;
                                                }
                                            }
                                        },this.L('编辑'));
                                btns.push(d);
                            }
                           if(this.persBtn.delete){
                                   var d1=h('Button',{
                                            props:{
                                                type:'error',
                                                size:'small'
                                            },
                                            on:{
                                                click:async()=>{
                                                    this.$Modal.confirm({
                                                        title:this.L(''),
                                                        content:this.L('删除这个数据吗？'),
                                                        okText:this.L('是'),
                                                        cancelText:this.L('否'),
                                                        onOk:async()=>{
                                                            await this.$store.dispatch({
                                                                type:'plotratio/delete',
                                                                data:this.plotratios[params.index]
                                                            })
                                                            this.$Message.success('删除成功！');
                                                            await this.getpage();
                                                        }
                                                    })
                                                }
                                            }
                                        },this.L('删除'));
                                   btns.push(d1); 
                            }
                            return h('div',btns);                   
                }
            }]
        }
    },
    computed:{
        plotratios(){
            return this.$store.state.plotratio.plotratios;
        },
        
        totalCount(){
            return this.$store.state.plotratio.totalCount;
        },
        currentPage(){
            return this.$store.state.plotratio.currentPage;
        },
        pageSize(){
            return this.$store.state.plotratio.pageSize;
        },
        
    },
    async created(){
        this.getpage();
        
    }
}
</script>




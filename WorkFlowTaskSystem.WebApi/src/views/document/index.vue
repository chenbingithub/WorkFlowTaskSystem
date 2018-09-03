

<template>
    <div >
    <Row>
            <Col span="6">
                <Card style="min-height:480px;max-height:480px;overflow-y:auto;">
                    
                   <Tree :data="documents" :load-data="loadData" @on-select-change="onselectchange"  ></Tree>
                </Card>
            </Col>
            <Col span="18" class="padding-left-10">
                <Card style="min-height:480px;">
                    
                    
                    <router-view></router-view>
                </Card>
            </Col>
    </Row>
      
    </div>
</template>

<script>
import Cookies from 'js-cookie';
export default {
    name: 'access_index',
    data () {
        return {
            editPermission:{},
            selectPermission:{},
        }
    },
    
    methods: {
   
 
         onselectchange(data){
           //this.selectOrganization=data.pop();
           //this.detailsOrganization=this.selectOrganization.data;
            this.$router.push({
                        name: 'organization'
                    });
        },
        
        async loadData (item, callback) {
                var data= await this.$store.dispatch({
                    type:'document/getdocumentFile',
                    data:item.id
                });
                callback(data);
            },
        async getpage(){
            await this.$store.dispatch({
                type:'document/getAll'
            });
            
        }
         
    },
     watch: {
            '$route' (to) {
                this.$store.commit('setCurrentPageName', to.name);
                let pathArr = util.setCurrentPath(this, to.name);
                localStorage.currentPageName = to.name;
            }
        },
    computed:{
        
        documents(){
            return this.$store.state.document.documentFiles;
        }
      


        
    },
    async created(){
        this.getpage();
       
    }
};
</script>
